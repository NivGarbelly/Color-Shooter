using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[ExecuteInEditMode]
public class Trophy : MonoBehaviour
{
    [SerializeField]private enum ObjColor
    {
        red = 0,
        green = 1,
        blue = 2
    };

    [SerializeField] private GameManeger _gameManeger;
    private int counter;
    [SerializeField] private Collider col;
    [SerializeField] private ParticleSystem boom;
    [SerializeField] private AudioSource boonmSound;
    [SerializeField] private ObjColor currentColor;
    [SerializeField] private  List<EnemyAIBase> _enemyAiBase;
    [SerializeField] private  GameObject[] walls;
    private bool isDisabele = false;
    private Renderer renderer;


    private void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        switch (currentColor)
        {
            case ObjColor.red:
                gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor",Color.red);
                break;
            case ObjColor.blue:
                gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor",Color.blue);
                break;
            case ObjColor.green:
                gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_BaseColor",Color.green);
                break;
        }
    }

    private void Start()
    {
        foreach (var enemyAiBase in FindObjectsOfType<EnemyAIBase>()) _enemyAiBase.Add(enemyAiBase);
    }


    void FixedUpdate()
    {
        counter = 0;
        foreach (var enemy in _gameManeger.enemies)
        {
            var enemmyRenderer = enemy.GetComponent<Renderer>();
            if (enemmyRenderer.material.GetColor("_BaseColor") == renderer.material.GetColor("_BaseColor"))
            {
                counter++;
            }
        }

        if (isDisabele == false)
        {
            if (counter >= _gameManeger.enemies.Count)
            {
                col.isTrigger = true;
                renderer.material.SetFloat("_Smoothness", 0.445f);
            }
            else if (counter < _gameManeger.enemies.Count)
            {
                renderer.material.SetFloat("_Smoothness", 0f);
                col.isTrigger = false;
            }
        }

        if (_gameManeger.Trophies.Count == 1) 
        {
            var mainModule = boom.main;
            mainModule.startColor = Color.white;    
        }

        if (_gameManeger.Trophies.Count > 1)
        {
                var Render = GetComponent<Renderer>();
                Color curCol = Render.material.GetColor("_BaseColor");
                var mainModule = boom.main;
                mainModule.startColor = curCol;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _gameManeger.Trophies.Count == 1)
        {
            _gameManeger.enemiesResetColors();
            boom.Play();
            boonmSound.Play();
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.white);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Smoothness",0.445f);
            isDisabele = true;
            gameObject.GetComponent<Collider>().enabled = false;
            walls = GameObject.FindGameObjectsWithTag("Walls");
            foreach (var wall in walls)
            {
                wall.GetComponent<WallScript>().DesAnim();
            }
            foreach (var enemyAi in _enemyAiBase)
            {
                enemyAi.EnemyLose();
            }

            _gameManeger.Win();
        }

        if (other.gameObject.CompareTag("Player") && _gameManeger.Trophies.Count > 1)
        {
            boonmSound.Play();
            boom.Play();
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.white);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Smoothness",0.445f);
            gameObject.GetComponent<Collider>().enabled = false;
            isDisabele = true;
            _gameManeger.Trophies.Remove(this.gameObject);
        }
    }
    public void TrophyCreated()
    {
        _gameManeger.CreatePalyer();
    }
}

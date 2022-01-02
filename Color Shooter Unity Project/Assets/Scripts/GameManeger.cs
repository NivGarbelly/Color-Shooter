using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class GameManeger : MonoBehaviour
{
    public static List<Color> colors = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.blue,
    };

    [SerializeField] public List<GameObject> Trophies = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    private Renderer enemiesRendere;
    public List<Color> enemiesColors = new List<Color>();
    public static int bulletNextColor;
    [SerializeField] private CanvasManeger _canvasManeger;
    [FormerlySerializedAs("isWin")] public bool isPaused = false;
    public int redCount;
    public int greenCount;
    public int blueCount;
    public int whiteCount;

    void Awake()
    {
        isPaused = false;
        foreach (var trophyObj in GameObject.FindGameObjectsWithTag("Trophies"))
        {
            Trophies.Add(trophyObj);
        }

        foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemies"))
        {
            enemies.Add(enemyObj);
        }

        AddColorToList();
        nextColorSet();
        HUDTEST();
    }

    private void FixedUpdate()
    {
        if (enemies.Count != 0)
        {
            if (Trophies.Count == 0)
            {
                isPaused = true;
                
            }
        }
    }

    public void AddColorToList()
    {
        foreach (var enemy in enemies)
        {
            enemiesRendere = enemy.GetComponent<Renderer>();
            Color color = enemiesRendere.material.GetColor("_BaseColor");
            enemiesColors.Add(color);
        }
    }

    public void enemiesResetColors()
    {
        foreach (var enemy in enemies)
        {
                var enemiesRenderes = GetComponentsInChildren<Renderer>();
                foreach (var VARIABLE in enemiesRenderes)
                {
                    VARIABLE.material.SetColor("_BaseColor", Color.white);
                    VARIABLE.material.SetFloat("_Smoothness", 0f); 
                }
        }
    }

    public void nextColorSet()
    {
        bulletNextColor = Random.Range(0, colors.Count);
    }

    
    public void HUDTEST()
    {
        foreach (var color in enemiesColors)
        {
            if (color == Color.blue)
            {
                blueCount++;
            }
            if (color == Color.green)
            {
                greenCount++;
            }
            if (color == Color.red)
            {
                redCount++;
            }
            if (color == Color.white)
            {
                whiteCount++;
            }
        }
    }

    public void HUDreset()
    {
        blueCount = 0;
        greenCount = 0;
        redCount = 0;
        whiteCount = 0;
    }

    public void Win()
    {
        FindObjectOfType<Camera>().GetComponent<CinemachineBrain>().enabled = false;
        
        FindObjectOfType<PlayerController>().GetComponentInChildren<Animation>().CrossFade("Win");
    }

    public void WinInvoked()
    {
        isPaused = true;
        _canvasManeger.nextLevel();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool isWin = false;
    public int redCount;
    public int greenCount;
    public int blueCount;
    public int whiteCount;

    void Awake()
    {
        isWin = false;
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
                isWin = true;
                _canvasManeger.nextLevel();
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
        Invoke("WinInvoked",1f);
    }

    private void WinInvoked()
    {
        isWin = true;
        _canvasManeger.nextLevel();
    }
}

using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class GameManeger : MonoBehaviour
{
      public enum GameState
    {
        SeupAnimations = 0,
        Start = 1,
        Pause = 2,
        Win = 3,
        Lost = 4
    }
    public GameState CurrentState;
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
    public int redCount;
    public int greenCount;
    public int blueCount;
    public int whiteCount;
    AnimationManager animationManager;



    void Awake()
    {
        animationManager = GetComponentInChildren<AnimationManager>();
        Application.targetFrameRate = 150;
        foreach (var trophyObj in GameObject.FindGameObjectsWithTag("Trophies"))
        {
            Trophies.Add(trophyObj);
        }

        foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemies"))
        {
            enemies.Add(enemyObj);
        }

        AddColorToList();
        HUDTEST();
        nextColorSet();
        DoAccordingToState(GameState.SeupAnimations);
    }
    
    public void CheckWin()
    {
    if (enemies.Count != 0)
        {
            if (Trophies.Count == 0)
            {
              DoAccordingToState(GameState.Win);
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

    public void DoAccordingToState(GameState gameState)
    {
        CurrentState = gameState;
            switch (CurrentState)
            {
                case GameState.SeupAnimations:
                    animationManager.AnimateWalls();
                    break;
                default:
                case GameState.Start:
                    StartGame();
                    break;

                case GameState.Pause:
                    PauseGame();
                    break;

                case GameState.Win:
                //Win 
                    break;
                
                case GameState.Lost:
                //LOSTs 
                    break;
            }
    }
    private void SetupEandTGame()
    {
        foreach (var trophyObj in GameObject.FindGameObjectsWithTag("Trophies"))
        {
          trophyObj.GetComponent<Animation>().CrossFade("CreatTrophy");
        }

        foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemies"))
        {
             enemyObj.GetComponent<Animation>().CrossFade("Start");
        }
    }
    private void StartGame()
    {

    }
    private void PauseGame()
    {

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
        FindObjectOfType<PlayerController>().GetComponentInChildren<Animation>().CrossFade("Win");
    }

    public void Lost()
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyAIBase>().playerDead = true;
        }
    }
   
    public void CreatePalyer()
    {
        FindAnyObjectByType<PlayerController>().CreateAnim();
    }
}
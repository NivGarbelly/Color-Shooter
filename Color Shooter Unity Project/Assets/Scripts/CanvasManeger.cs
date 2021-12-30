using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManeger : MonoBehaviour
{
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject MainMenu;
    public bool isGamePaused = false;
    public bool onSettingMenu = false;
    public GameObject pauseMenu;
    public int currentScene;
    public Canvas HUD;
    public TextMeshProUGUI Red;
    public TextMeshProUGUI Blue;
    public TextMeshProUGUI Green;
    public TextMeshProUGUI White;
    public GameManeger gameManeger;
    public GameObject loadingScreen;
    public Slider slider;


    private void Awake()
    {
        isGamePaused = false;
        onSettingMenu = false;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void FixedUpdate()
    {
        if (MainMenu == null)
        {
            Red.SetText(gameManeger.redCount.ToString());
            Blue.SetText(gameManeger.blueCount.ToString());
            Green.SetText(gameManeger.greenCount.ToString()); 
            White.SetText(gameManeger.whiteCount.ToString());
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void openSettingMenu()
    {
        Settings.active = true;
        onSettingMenu = true;
    }
    
    public void closeSetting()
    {
        Settings.active = false;
        onSettingMenu = false;
    }
    public void Pause()
    {
        pauseMenu.active = true;
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenu.active = false;
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void goToMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
    public void newGame()
    {
        LoadLevel(1);
        Time.timeScale = 1f;
    }

    public void restartLevel()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(currentScene);
        Time.timeScale = 1f;
    }

    public void nextLevel()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(currentScene + 1);
        Time.timeScale = 1f;
    }
    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenu == null)
            {
              if (isGamePaused == false && onSettingMenu == false) 
              {
                              Pause(); 
              }
              else if(isGamePaused == true && onSettingMenu == false) 
              { 
                              Resume(); 
              }  
            }
            
            if (onSettingMenu == true)
            {
                closeSetting();
            }
        }
    }
   
}

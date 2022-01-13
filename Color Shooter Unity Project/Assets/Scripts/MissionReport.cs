using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionReport : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameManeger gameManeger;
    public List<GameObject> tutorials = new List<GameObject>();
    public int counter;
    public Animation playerAnim;
    void Start()
    {
        gameManeger.isPaused = true;
        Time.timeScale = 0f;
        counter = 1;
    }

    public void pauseGame()
    {
        gameManeger.isPaused = true;
        Time.timeScale = 0f;
        tutorials[counter-1].gameObject.SetActive(true);
    }

    public void Continue()
    {
        if (counter != tutorials.Count)
        {
            tutorials[counter-1].gameObject.SetActive(false);
            Time.timeScale = 1f;
            counter++;
            Invoke("pauseGame",0.1f);
        }
        else if (counter == tutorials.Count)
        {
            Time.timeScale = 1f;
            tutorials[counter-1].gameObject.SetActive(false);
            playerAnim.CrossFade("Opening");
            Destroy(this);
        }
        
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            Continue();
        }
    }
}

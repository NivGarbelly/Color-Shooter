using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSettings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private AudioSource gameMusic;
    public static float vol;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {

        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        if (sceneNum!=0)
        {
            gameMusic.volume = vol;
            mainMenuMusic.mute = true;
            gameMusic.mute = false;
        }
        else
        {
            mainMenuMusic.volume = vol;
            mainMenuMusic.mute = false;
            gameMusic.mute = true;
        }
    }
}

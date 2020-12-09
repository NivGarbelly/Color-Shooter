using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource Music;
    public static bool musicEnabled = true;

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
        if (musicEnabled==true)
        {
            Music.enabled = true;
        }
        else
        {
            Music.enabled = false;
        }
    }
}

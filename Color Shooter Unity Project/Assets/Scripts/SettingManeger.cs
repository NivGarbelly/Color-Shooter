using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManeger : MonoBehaviour
{
    

    public void Res1080()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void Res900()
    {
        Screen.SetResolution(1660, 900, true);
    }
    public void Res720()
    {
        Screen.SetResolution(1280, 720, true);
    }
    public void Res600()
    {
        Screen.SetResolution(800, 600, true);
    }

    public void musicOn()
    {
        MusicSettings.musicEnabled = true;
    }
    
    public void musicOff()
    {
        MusicSettings.musicEnabled = false;
    }
}

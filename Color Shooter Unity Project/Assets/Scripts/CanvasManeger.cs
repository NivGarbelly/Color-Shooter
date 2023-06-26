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
    public Canvas HUD;
    public TextMeshProUGUI Red;
    public TextMeshProUGUI Blue;
    public TextMeshProUGUI Green;
    public TextMeshProUGUI White;
    public GameManeger gameManeger;

    private void Start() 
    {
    HUDupdate();
    }
public void HUDupdate()
{
            Red.SetText(gameManeger.redCount.ToString());
            Blue.SetText(gameManeger.blueCount.ToString());
            Green.SetText(gameManeger.greenCount.ToString()); 
            White.SetText(gameManeger.whiteCount.ToString());
}
  
}

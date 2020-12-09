using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionReport : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameManeger gameManeger;
    void Start()
    {
        gameManeger.isWin = true;
        Time.timeScale = 0f;
    }

    public void ButtonClick()
    {
        gameManeger.isWin = false;
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AnimtionEvent : MonoBehaviour
{
    public void AfterWinAnim()
    {
        GameManeger gameManeger = FindObjectOfType<GameManeger>();
        gameManeger.WinInvoked();
    }
    public void Opening()
    {
        FindObjectOfType<Camera>().GetComponent<CinemachineBrain>().enabled = true;
       FindObjectOfType<PlayerController>().isLose = false;
    }
}

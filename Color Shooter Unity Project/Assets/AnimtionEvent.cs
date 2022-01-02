using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtionEvent : MonoBehaviour
{
    public void AfterWinAnim()
    {
        GameManeger gameManeger = FindObjectOfType<GameManeger>();
        gameManeger.WinInvoked();
    }
}

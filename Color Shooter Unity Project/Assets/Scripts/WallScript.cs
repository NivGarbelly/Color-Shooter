﻿using System;
using UnityEngine;
[ExecuteInEditMode]
public class WallScript : MonoBehaviour
{
    public bool coloredWall= false;
    private enum ObjColor
    {
        red = 0,
        green = 1,
        blue = 2,
    };
    [SerializeField] private ObjColor currentColor;
    private GameManeger gameManeger;

    private void Awake()
    {
        gameManeger= FindObjectOfType<GameManeger>();
        GetComponent<Animation>().CrossFade("Start");
        if (coloredWall == true)
        {
            switch (currentColor)
            {
                case ObjColor.red:
                    gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.red);
                    break;
                case ObjColor.blue:
                    gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.blue);
                    break;
                case ObjColor.green:
                    gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.green);
                    break;
            } 
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor",Color.white);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (coloredWall == true)
        {
            if (other.gameObject.CompareTag("Bullets"))
            {
                if (other.gameObject.GetComponents<Renderer>() != null)
                {
                    var otheRenderer = other.GetComponent<Renderer>();
                    var cubeRenderer = GetComponent<Renderer>();
                    if (otheRenderer.material.GetColor("_BaseColor") ==cubeRenderer.material.GetColor("_BaseColor"))
                    {
                        DesAnim();
                    }
                }
            }  
        }
    }

    public void DesAnim()
    {
        GetComponent<Animation>().CrossFade("Destroy");
        GetComponent<Collider>().enabled = false;
    }

    public void DesObj()
    {
        Destroy(this.gameObject);
    }

    public void WallCreated()
    {
        gameManeger.CreateEnemiesandTrophies();
    }
}

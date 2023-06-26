using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField]private ParticleSystem hitBoom;
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        var render = GetComponent<Renderer>();
        render.material.SetColor("_BaseColor", GameManeger.colors[GameManeger.bulletNextColor]);
        hitBoom.startColor = GameManeger.colors[GameManeger.bulletNextColor];
        Color color = render.material.GetColor("_BaseColor");
        if (color==Color.blue)
        {
            audioSource.pitch = 2;
        }
        
        if (color==Color.green)
        {
            audioSource.pitch = 3;
        }
        
        if (color==Color.red)
        {
            audioSource.pitch = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var rend = GetComponent<Renderer>();
        var col = GetComponent<Collider>();
        rend.enabled = false;
        col.enabled = false;
        hitBoom.Play();
        Invoke("destroy",0.5f);
    }

    private void destroy()
    {
       Destroy(this.gameObject);
    }
}

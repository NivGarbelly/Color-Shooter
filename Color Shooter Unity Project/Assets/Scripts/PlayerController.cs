using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 movement;
    [SerializeField] private Camera cam;
    private Vector3 mousePos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameManeger _gameManeger;
    [SerializeField] private GameObject colorIndicator;
    [SerializeField] private GameObject playerArt;
    [SerializeField] private CanvasManeger _canvasManeger;
    [SerializeField] private bool isLose = false;
    [SerializeField] private AudioSource movmenntSound;

    private void playerMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(-moveVertical, 0f, moveHorizontal);
        movement *= (speed * Time.deltaTime);
        transform.position += movement;
        if (moveHorizontal == 0 && moveVertical==0)
        {
            movmenntSound.Stop(); 
        }
        
        if(moveHorizontal != 0 || moveVertical!=0)
        {
            if (movmenntSound.isPlaying!=true)
            {
                movmenntSound.Play();
            }
        }
    }

    private void playerRotation()
    {
        var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.95f);
    }

    private void playerShoot()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (colorIndicator != null)
            {
                var tempBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                var rigid = tempBullet.GetComponent<Rigidbody>();
                rigid.velocity = -shootPoint.right * 7f;
                _gameManeger.nextColorSet();
                var render = colorIndicator.GetComponent<Renderer>();
                render.material.SetColor("_BaseColor", GameManeger.colors[GameManeger.bulletNextColor]);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            var col = GetComponent<Collider>();
            col.enabled = false;
            Destroy(colorIndicator);
            Destroy(playerArt);
            isLose = true;
            Invoke("desPlayer", 2f);
        }
    }

    private void desPlayer()
    {
        _canvasManeger.restartLevel();
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        isLose = false;
        var render = colorIndicator.GetComponent<Renderer>();
        render.material.SetColor("_BaseColor", GameManeger.colors[GameManeger.bulletNextColor]);
    }

    void FixedUpdate()
    {
        if (_gameManeger.isWin == false)
        {
            if (isLose == false)
            {
                if (_canvasManeger.isGamePaused == false)
                {
                    playerMovement();
                    playerRotation();
                }
            }
        }
    }

    private void Update()
    {
        if (_gameManeger.isWin == false)
        {
            if (isLose == false)
            {
                if (_canvasManeger.isGamePaused == false)
                {
                    playerShoot();
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AnimtionEvent : MonoBehaviour
{
    private void FixedUpdate()
    {
        var dir = Input.mousePosition - FindObjectOfType<Camera>().WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.up);
        Quaternion newRotNew= Quaternion.Euler(transform.rotation.x+100f*Input.GetAxisRaw("Horizontal"),transform.position.y,transform.rotation.z+100f*Input.GetAxisRaw("Vertical"));
        transform.rotation = Quaternion.Lerp(newRotNew, newRot, 0.9f);
        
    }

    public void AfterWinAnim()
    {
        GameManeger gameManeger = FindObjectOfType<GameManeger>();
        gameManeger.WinInvoked();
    }
    public void Opening()
    { 
      var cmCam = FindObjectOfType<CinemachineVirtualCameraBase>();
      cmCam.Follow = this.transform;
      FindObjectOfType<PlayerController>().isLose = false;
    }

    public void desPlayerFun()
    {
        FindObjectOfType<PlayerController>().desPlayer();
    }
}

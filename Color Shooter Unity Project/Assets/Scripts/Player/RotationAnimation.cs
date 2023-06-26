using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class RotationAnimation : MonoBehaviour
{
    private void FixedUpdate()
    {
        var dir = Input.mousePosition - FindObjectOfType<Camera>().WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.up);
        Quaternion newRotNew= Quaternion.Euler(transform.rotation.x+100f*Input.GetAxisRaw("Horizontal"),transform.position.y,transform.rotation.z+100f*Input.GetAxisRaw("Vertical"));
        transform.rotation = Quaternion.Lerp(newRotNew, newRot, 0.9f);
    }
}

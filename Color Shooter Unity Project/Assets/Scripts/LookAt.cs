using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.125f;
    public Vector3 offset;
    private void LateUpdate()
    {
        if (player != null)
        {
         Vector3 desiredPos = player.position + offset;
         Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smooth);
         transform.position = smoothedPos;
        }
    }
}
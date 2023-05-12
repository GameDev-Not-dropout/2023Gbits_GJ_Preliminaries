using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float bottomBoundary;
    public float topBoundary;

    const int APosX = 0;
    const int BPosX = 80;

    private void LateUpdate()
    {
        if (player.position.y <= bottomBoundary || player.position.y >= topBoundary)
            return;
        else
            transform.position = new Vector3(transform.position.x, player.position.y, -10);
    }








}

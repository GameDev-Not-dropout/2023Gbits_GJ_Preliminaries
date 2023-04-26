using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    Camera main;

    private void Start()
    {
        main = Camera.main;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, main.transform.position.y, -10);
    }









}

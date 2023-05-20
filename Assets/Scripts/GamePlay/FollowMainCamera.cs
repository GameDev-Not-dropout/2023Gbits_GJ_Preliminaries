using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    Camera mainCam;
    Camera thisCam;

    private void Start()
    {
        mainCam = Camera.main;
        thisCam = transform.GetComponent<Camera>();
        thisCam.aspect = mainCam.aspect;

        thisCam.orthographicSize = thisCam.orthographicSize * 1920 / 1080 * Screen.height / Screen.width;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, mainCam.transform.position.y, -10);
    }
}
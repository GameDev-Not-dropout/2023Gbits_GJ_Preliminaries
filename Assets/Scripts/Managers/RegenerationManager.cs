using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationManager : MonoBehaviour
{
    public static RegenerationManager Instance;
    Transform RegenerationPoint;

    Camera mainCam;
    public Camera scene2Cam;
    public Transform player;
    Vector3 mainCamPoint;
    Vector3 scene2CamPoint;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnRegenerationPointRef, RecordRegenerationPoint);
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, Regeneration);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnRegenerationPointRef, RecordRegenerationPoint);
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, Regeneration);

    }

    private void Start()
    {
        mainCam = Camera.main;
    }



    /// <summary>
    /// 记录重生点和摄像机位置
    /// </summary>
    public void RecordRegenerationPoint(Transform newRecord)
    {
        RegenerationPoint = newRecord;
        mainCamPoint = mainCam.transform.position;
        scene2CamPoint = scene2Cam.transform.position;
    }

    public void Regeneration(Transform playerPos)
    {
        PlayerDieCouroutine(playerPos);
    }

    void PlayerDieCouroutine(Transform playerPos)
    {
        SceneFadeManager.instance.RegenarationFadeWithTween(0f, 1f, () => MoveCam(playerPos));
    }
    
    void MoveCam(Transform playerPos)
    {
        if (mainCamPoint.x < 40)
        {
            mainCam.transform.position = new Vector3(0f, mainCam.transform.position.y, -10);
        }
        else
        {
            mainCam.transform.position = new Vector3(80f, mainCam.transform.position.y, -10);
        }
        scene2Cam.transform.position = scene2CamPoint;

        playerPos.position = RegenerationPoint.position;

        SceneFadeManager.instance.RegenarationFadeWithTween(1f, 0f, () => EventSystem.Instance.EmitEvent(EventName.OnSceneFadeEnd));

    }

}

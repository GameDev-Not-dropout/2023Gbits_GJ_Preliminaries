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
    Vector3 mainCamPoint;
    Vector3 scene2CamPoint;
    public CinemachineConfiner2D confiner2D;
    public PolygonCollider2D polygonCollider1;
    public PolygonCollider2D polygonCollider2;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventSystem.instance.AddEventListener<Transform>(EventName.OnRegenerationPointRef, RecordRegenerationPoint);
        EventSystem.instance.AddEventListener<Transform>(EventName.OnPlayerDie, Regeneration);
    }

    private void OnDisable()
    {
        EventSystem.instance.RemoveEventListener<Transform>(EventName.OnRegenerationPointRef, RecordRegenerationPoint);
        EventSystem.instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, Regeneration);

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
        if (mainCamPoint.x < 40)
        {
            confiner2D.m_BoundingShape2D = polygonCollider1;
        }
        else
        {
            confiner2D.m_BoundingShape2D = polygonCollider2;
        }
        mainCam.transform.position = mainCamPoint;
        scene2Cam.transform.position = scene2CamPoint;

        playerPos.position = RegenerationPoint.position;
    }



}

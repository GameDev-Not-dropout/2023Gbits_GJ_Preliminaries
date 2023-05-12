using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

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

        if (SceneManager.GetActiveScene().buildIndex != 3 && SceneManager.GetActiveScene().buildIndex != 4)
            return;

        if (RegenerationPoint.position.x < 40)      // 玩家在场景1
        {
            if (RegenerationPoint.position.x < TransitionManager.Instance.TriggerAPos)    // 此时在线的左边
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 1);
            else
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 2);
        }
        else   // 玩家在场景2
        {
            if (RegenerationPoint.position.x < TransitionManager.Instance.TriggerBPos)    // 此时在线的左边
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 3);
            else
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 4);
        }
    }

}

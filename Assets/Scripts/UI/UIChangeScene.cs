using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIChangeScene : MonoBehaviour
{
    Camera mainCamera;
    public bool isChapther3;
    public CinemachineConfiner2D confiner2D;
    public Camera sceneCamera2;
    public PolygonCollider2D polygonCollider1;
    public PolygonCollider2D polygonCollider2;
    Vector3 mainCameraPos = new Vector3(0, 0, -10);
    Vector3 sceneCamera2Pos = new Vector3(80, 0, -10);

    public SpriteRenderer Left_SceneBG;
    public SpriteRenderer Right_SceneBG;
    public Sprite A_SceneSprite;
    public Sprite B_SceneSprite;

    bool canChangeScene;
    float lastChangeTime;

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnChangeScene, ChangeScene);
        if (isChapther3)
        {
            EventSystem.Instance.AddEventListener(EventName.OnChangeCamera, ChangeCameraPos);
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            canChangeScene = true;
        }

        mainCamera = Camera.main;
    }
    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnChangeScene, ChangeScene);
        if (isChapther3)
        {
            EventSystem.Instance.RemoveEventListener(EventName.OnChangeCamera, ChangeCameraPos);
        }
    }

    void ChangeScene(Transform plyaerTransform)
    {
        if (!canChangeScene)
            return;
        if (Time.time - lastChangeTime <= 1f)   // 切换场景需要1s时间间隔
            return;

        lastChangeTime = Time.time;
        Vector3 pos = plyaerTransform.position;

        if (pos.x < 40)      // 玩家在场景1
        {
            if (pos.x < TransitionManager.Instance.TriggerAPos)    // 此时在线的左边
            {
                pos.x = pos.x + 80;
                plyaerTransform.position = pos;
                confiner2D.m_BoundingShape2D = polygonCollider2;
                mainCamera.transform.position = sceneCamera2Pos;
                Left_SceneBG.sprite = A_SceneSprite;  // 切换为A场景图片
                // A场景左边的平台停止移动，同时B场景左边的平台开始移动
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 1);
            }
            else
            {
                pos.x = pos.x + 80;
                plyaerTransform.position = pos;
                sceneCamera2.transform.position = sceneCamera2Pos;
                Right_SceneBG.sprite = A_SceneSprite;
                // A场景右边的平台停止移动，同时B场景右边的平台开始移动
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 2);

            }

        }
        else   // 玩家在场景2
        {
            if (pos.x < TransitionManager.Instance.TriggerBPos)    // 此时在线的左边
            {
                pos.x = pos.x - 80;
                plyaerTransform.position = pos;
                confiner2D.m_BoundingShape2D = polygonCollider1;
                mainCamera.transform.position = mainCameraPos;
                Left_SceneBG.sprite = B_SceneSprite;  // 切换为B场景图片
                // B场景左边的平台停止移动，同时A场景左边的平台开始移动
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 3); 

            }
            else
            {
                pos.x = pos.x - 80;
                plyaerTransform.position = pos;
                sceneCamera2.transform.position = mainCameraPos;
                Right_SceneBG.sprite = B_SceneSprite;
                // B场景右边的平台停止移动，同时A场景右边的平台开始移动
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 4); 

            }
        }

        SoundManager.Instance.PlaySound(SE.changeScene);

        if (mainCamera.transform.position.x > 40 && sceneCamera2.transform.position.x < 40)
        {
            Left_SceneBG.sprite = B_SceneSprite;
            Right_SceneBG.sprite = A_SceneSprite;
        }
    }

    void ChangeCameraPos()
    {
        if (mainCamera.transform.position.x < 40)
        {
            confiner2D.m_BoundingShape2D = polygonCollider2;
            mainCamera.transform.position = sceneCamera2Pos;
            sceneCamera2.transform.position = mainCameraPos;
        }
        else
        {
            confiner2D.m_BoundingShape2D = polygonCollider1;
            mainCamera.transform.position = mainCameraPos;
            sceneCamera2.transform.position = sceneCamera2Pos;
        }
    }




}

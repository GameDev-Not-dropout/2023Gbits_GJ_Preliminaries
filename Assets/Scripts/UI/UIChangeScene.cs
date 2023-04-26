using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeScene : MonoBehaviour
{
    Camera mainCamera;
    public CinemachineConfiner2D confiner2D;
    public Camera sceneCamera2;
    public PolygonCollider2D polygonCollider1;
    public PolygonCollider2D polygonCollider2;
    Vector3 mainCameraPos = new Vector3(0, 0, -10);
    Vector3 sceneCamera2Pos = new Vector3(80, 0, -10);

    public Image Left_SceneBG;
    public Image Right_SceneBG;
    public Sprite A_SceneSprite;
    public Sprite B_SceneSprite;


    private void OnEnable()
    {
        EventSystem.instance.AddEventListener<Transform>(EventName.OnChangeScene, ChangeScene);
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnDisable()
    {
        EventSystem.instance.RemoveEventListener<Transform>(EventName.OnChangeScene, ChangeScene);
    }

    void ChangeScene(Transform plyaerTransform)
    {
        Vector3 pos = plyaerTransform.position;

        if (pos.x < 40)      // 玩家在场景1
        {
            if (pos.x < TransitionManager.Instance.TriggerAPos)    // 此时在线的左边
            {
                pos.x = pos.x + 80;
                plyaerTransform.position = pos;
                confiner2D.m_BoundingShape2D = polygonCollider2;
                mainCamera.transform.position = sceneCamera2Pos;
                Left_SceneBG.overrideSprite = A_SceneSprite;  // 切换为A场景图片
            }
            else
            {
                pos.x = pos.x + 80;
                plyaerTransform.position = pos;
                sceneCamera2.transform.position = sceneCamera2Pos;
                Right_SceneBG.overrideSprite = A_SceneSprite;
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
                Left_SceneBG.overrideSprite = B_SceneSprite;  // 切换为B场景图片
            }
            else
            {
                pos.x = pos.x - 80;
                plyaerTransform.position = pos;
                sceneCamera2.transform.position = mainCameraPos;
                Right_SceneBG.overrideSprite = B_SceneSprite;
            }
        }
    }






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    Camera mainCamera;
    public Camera sceneCamera2;
    public Transform playerTransform;

    Vector3 GetHandlerScreenPoint => RectTransformUtility.WorldToScreenPoint(null, transform.position);

    private void Start()
    {
        mainCamera = Camera.main;
        this.SaveHandlerData();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        this.SaveHandlerData();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.DraggingHandler(eventData);
        TransitionScene();
    }

    /// <summary>
    /// 跳转场景
    /// </summary>
    private void TransitionScene()
    {
        Vector3 pos = playerTransform.position;
        if (pos.x < 40)    // 当前玩家在场景1
        {
            // 跳转到场景2
            Vector3 point = mainCamera.WorldToScreenPoint(pos);

            if (GetHandlerScreenPoint.x < point.x)
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x + 80;
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                playerTransform.position = pos;
                return;
            }
        }
        if (pos.x > 40 && pos.x < 110)    // 玩家在场景2
        {
            // 回到场景1
            Vector3 point = sceneCamera2.WorldToScreenPoint(pos);

            if (GetHandlerScreenPoint.x > point.x)
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x - 80;
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                playerTransform.position = pos;
                return;
            }

        }
    }

    /// <summary>
    /// 线跟随鼠标
    /// </summary>
    private void DraggingHandler(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.offsetMin = new Vector2(pos.x, -(Screen.height / 2));
        this.SaveHandlerData();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        this.SaveHandlerData();
    }

    /// <summary>
    /// 转为世界坐标，方便角色移动时判断是否越界
    /// </summary>
    private void SaveHandlerData()
    {
        TransitionManager.Instance.handler2 = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
    }
}

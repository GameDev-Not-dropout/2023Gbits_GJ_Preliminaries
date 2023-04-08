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
    public Camera sceneCamera3;
    public Transform playerTransform;
    public int handlerIndex;

    Vector3 GetHandlerScreenPoint => RectTransformUtility.WorldToScreenPoint(null, transform.position);

    private void Start()
    {
        mainCamera = Camera.main;
        this.SaveHandlerData();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        #region 防止两条线之间出现卡顿，弹出来一点
        if (handlerIndex == 2)
        {
            if (TransitionManager.Instance.handler2 >= TransitionManager.Instance.handler3 - 0.5f)
                rectTransform.offsetMin = new Vector2(mainCamera.WorldToScreenPoint(new Vector3(TransitionManager.Instance.handler2 - 0.2f, 0f, 0f)).x, -(Screen.height / 2));
        }
        if (handlerIndex == 3)
        {
            if (TransitionManager.Instance.handler2 + 0.5f >= TransitionManager.Instance.handler3)
                rectTransform.offsetMin = new Vector2(mainCamera.WorldToScreenPoint(new Vector3(TransitionManager.Instance.handler3 + 0.2f, 0f, 0f)).x, -(Screen.height / 2));
        } 
        #endregion
        this.SaveHandlerData();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        if (handlerIndex == 2)
        {
            if (mainCamera.WorldToScreenPoint(new Vector3(TransitionManager.Instance.handler3 - 0.5f, 0f, 0f)).x > Input.mousePosition.x)
                this.DraggingHandler(eventData);
            else
                rectTransform.offsetMin = new Vector2(mainCamera.WorldToScreenPoint(new Vector3(TransitionManager.Instance.handler3 - 0.6f, 0f, 0f)).x, -(Screen.height / 2));

        }
        if (handlerIndex == 3)
        {
            if (mainCamera.WorldToScreenPoint(new Vector3(TransitionManager.Instance.handler2 + 0.5f, 0f, 0f)).x < Input.mousePosition.x)
                this.DraggingHandler(eventData);
            else
                rectTransform.offsetMin = new Vector2(mainCamera.WorldToScreenPoint(new Vector3(TransitionManager.Instance.handler2 + 0.6f, 0f, 0f)).x, -(Screen.height / 2));

        }

        if (playerTransform.position.x < 10)    // 当前玩家在场景1
        {
            if (handlerIndex == 2)      // 跳转到场景2
            {
                Vector3 point = mainCamera.WorldToScreenPoint(playerTransform.position);

                if (GetHandlerScreenPoint.x < point.x)
                {
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 1);
                    playerTransform.position = new Vector3(playerTransform.position.x + 40, playerTransform.position.y);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 2);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 3, 40);
                    return;
                }
            }
        }
        if (playerTransform.position.x > 10 && playerTransform.position.x < 50)    // 玩家在场景2
        {
            if (handlerIndex == 3)      // 跳转到场景3
            {
                Vector3 point = sceneCamera2.WorldToScreenPoint(playerTransform.position);

                if (GetHandlerScreenPoint.x < point.x)
                {
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 1);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 3, -40);
                    playerTransform.position = new Vector3(playerTransform.position.x + 40, playerTransform.position.y);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 2);
                    return;
                }
            }
            if (handlerIndex == 2)      // 回到场景1
            {
                Vector3 point = sceneCamera2.WorldToScreenPoint(playerTransform.position);

                if (GetHandlerScreenPoint.x > point.x)
                {
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 1);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 3, 40);
                    playerTransform.position = new Vector3(playerTransform.position.x - 40, playerTransform.position.y);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 2);
                    return;
                }
            }
        }
        if (playerTransform.position.x > 50)
        {
            if (handlerIndex == 3)
            {
                Vector3 point = sceneCamera3.WorldToScreenPoint(playerTransform.position);

                if (GetHandlerScreenPoint.x > point.x)
                {
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 1);
                    playerTransform.position = new Vector3(playerTransform.position.x - 40, playerTransform.position.y);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 2);
                    TransitionManager.Instance.SpawnShockWaves(playerTransform.position, 3, -40);
                    return;
                }
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
        if (handlerIndex == 2)
        {
            TransitionManager.Instance.handler2 = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
        }
        if (handlerIndex == 3)
        {
            TransitionManager.Instance.handler3 = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
        }
    }
}

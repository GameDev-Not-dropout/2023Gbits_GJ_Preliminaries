using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    Camera mainCamera;
    public Camera sceneCamera2;
    public Camera sceneCamera3;
    public Transform playerTransform;
    public int handlerIndex;

    bool hasTransition;
    bool hasResume;

    private void Start()
    {
        mainCamera = Camera.main;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if (handlerIndex == 2)
        {
            TransitionManager.Instance.handler2 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        }
        if (handlerIndex == 3)
        {
            TransitionManager.Instance.handler3 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.offsetMin = new Vector2(pos.x, -(Screen.height / 2));


        if (playerTransform.position.x < 10)    // 当前玩家在场景1
        {
            if (handlerIndex == 2)      // 跳转到场景2
            {
                Vector3 point = mainCamera.WorldToScreenPoint(playerTransform.position);

                if (Input.mousePosition.x < point.x)
                {
                    TransitionManager.Instance.handler2 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                    playerTransform.position = new Vector3(playerTransform.position.x + 20, playerTransform.position.y);
                    //hasTransition = true;

                    return;
                }
            }
        }
        if (playerTransform.position.x > 10 && playerTransform.position.x < 30)    // 玩家在场景2
        {
            //if (hasTransition) return;

            if (handlerIndex == 3)      // 跳转到场景3
            {
                Vector3 point = sceneCamera2.WorldToScreenPoint(playerTransform.position);

                if (Input.mousePosition.x < point.x)
                {
                    TransitionManager.Instance.handler3 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                    playerTransform.position = new Vector3(playerTransform.position.x + 20, playerTransform.position.y);

                    return;

                }
            }
            if (handlerIndex == 2)      // 回到场景1
            {
                Vector3 point = sceneCamera2.WorldToScreenPoint(playerTransform.position);

                if (Input.mousePosition.x > point.x)
                {
                    TransitionManager.Instance.handler2 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                    playerTransform.position = new Vector3(playerTransform.position.x - 20, playerTransform.position.y);

                    //hasResume = true;
                    return;

                }
            }
        }
        if (playerTransform.position.x > 30)
        {
            if (handlerIndex == 3)
            {
                Vector3 point = sceneCamera3.WorldToScreenPoint(playerTransform.position);

                if (Input.mousePosition.x > point.x)
                {
                    TransitionManager.Instance.handler3 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                    playerTransform.position = new Vector3(playerTransform.position.x - 20, playerTransform.position.y);

                    return;

                }
            }
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        //hasTransition = false;
        //hasResume = false;
        if (handlerIndex == 2) 
        {
            TransitionManager.Instance.handler2 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        }
        if (handlerIndex == 3)
        {
            TransitionManager.Instance.handler3 = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        }
        //canvasGroup.blocksRaycasts = true;

    }

}

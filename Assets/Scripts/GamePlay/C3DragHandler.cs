using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class C3DragHandler : MonoBehaviour
{
    public RectTransform rectTransform;
    public RectTransform rawImageRect;
    public Transform playerTransform;
    public float autoMoveSpeed;

    RectTransform thisRect;
    Camera mainCamera;
    bool initTransitonTrigger;
    float timer = 2f;
    bool isAlive = true;
    float screenWidth;
    float screenHeight;

    private Vector3 GetHandlerScreenPoint => RectTransformUtility.WorldToScreenPoint(null, transform.position);

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, screenHeight);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
        rawImageRect.sizeDelta = new Vector2(screenWidth, screenHeight);
        rawImageRect.anchoredPosition = new Vector2(-screenWidth / 2, 0f);

        mainCamera = Camera.main;
        //this.SaveHandlerData();
        thisRect = this.GetComponent<RectTransform>();
        TransitionManager.Instance.TriggerAPos = 0;
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, OnPlayerDie);
        EventSystem.Instance.AddEventListener(EventName.OnSceneFadeEnd, OnSceneFadeEnd);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, OnPlayerDie);
        EventSystem.Instance.RemoveEventListener(EventName.OnSceneFadeEnd, OnSceneFadeEnd);
    }

    private void OnPlayerDie(Transform trans)
    {
        isAlive = false;
    }

    private void OnSceneFadeEnd()
    {
        transform.position = new Vector3(screenWidth - 20f, transform.position.y);
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            if (!initTransitonTrigger)
            {
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }
                initTransitonTrigger = true;
            }

            Vector3 pos = transform.position;
            if (pos.x <= 20)    // 限制线的移动
            {
                transform.position = new Vector3(screenWidth - 20f, pos.y);
                EventSystem.Instance.EmitEvent(EventName.OnChangeCamera);
            }
            else
                transform.position = new Vector2(pos.x - autoMoveSpeed * Time.deltaTime, pos.y);

            rectTransform.offsetMin = new Vector2(transform.position.x, -(screenHeight / 2));
            this.SaveHandlerData();
        }
    }

    private void SaveHandlerData()
    {
        if (mainCamera.transform.position.x < 40)
        {
            if (mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x <= -20.9f)
            {
                TransitionManager.Instance.TriggerAPos = 100.88f;
                return;
            }
            TransitionManager.Instance.TriggerAPos = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
        }
        else
        {
            if (mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x <= 59.15f)
            {
                TransitionManager.Instance.TriggerAPos = 20.88f;
                return;
            }
            TransitionManager.Instance.TriggerAPos = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.UseMoveCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.UseNormalCursor();
    }
}
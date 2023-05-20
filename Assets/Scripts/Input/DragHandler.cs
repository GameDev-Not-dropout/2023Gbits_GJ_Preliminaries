using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    public RectTransform rawImageRect;
    public Transform playerTransform;
    public bool canDrag = true;

    RectTransform thisRect;
    Camera mainCamera;
    float screenWidth;
    float screenHeight;

    private Vector3 GetHandlerScreenPoint => RectTransformUtility.WorldToScreenPoint(null, transform.position);

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        rectTransform.offsetMin = new Vector2(screenWidth / 2, -(screenHeight / 2));
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, screenHeight);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
        rawImageRect.sizeDelta = new Vector2(screenWidth, screenHeight);
        rawImageRect.anchoredPosition = new Vector2(-screenWidth / 2, 0f);

        mainCamera = Camera.main;
        //this.SaveHandlerData();
        thisRect = this.GetComponent<RectTransform>();
        TransitionManager.Instance.TriggerAPos = 0;
        TransitionManager.Instance.TriggerBPos = 80;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        if (!canDrag)
            return;
        this.SaveHandlerData();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (!canDrag)
            return;
        this.DraggingHandler(eventData);
    }

    /// <summary>
    /// 线跟随鼠标
    /// </summary>
    private void DraggingHandler(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.offsetMin = new Vector2(pos.x, -(screenHeight / 2));
        RectTransformUtility.ScreenPointToWorldPointInRectangle(thisRect, eventData.position, eventData.enterEventCamera, out pos);
        if (pos.x <= 20)    // 限制线的移动
        {
            transform.position = new Vector3(20f, transform.position.y);
        }
        else if (pos.x >= screenWidth - 20f)
        {
            transform.position = new Vector3(screenWidth - 20f, transform.position.y);
        }
        else
            transform.position = new Vector2(pos.x, transform.position.y);
        this.SaveHandlerData();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        if (!canDrag)
            return;
        this.SaveHandlerData();
    }

    /// <summary>
    /// 转为世界坐标，方便角色移动时判断是否越界
    /// </summary>
    private void SaveHandlerData()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            TransitionManager.Instance.TriggerAPos = 0;
            TransitionManager.Instance.TriggerBPos = 80;
            return;
        }

        if (mainCamera.transform.position.x < 40)
        {
            TransitionManager.Instance.TriggerAPos = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
            TransitionManager.Instance.TriggerBPos = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x + 80;
        }
        else
        {
            TransitionManager.Instance.TriggerAPos = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x - 80;
            TransitionManager.Instance.TriggerBPos = mainCamera.ScreenToWorldPoint(GetHandlerScreenPoint).x;
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
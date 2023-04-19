using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    Camera mainCamera;
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
}

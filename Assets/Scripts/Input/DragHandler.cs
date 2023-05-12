using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    RectTransform thisRect;
    Camera mainCamera;
    public Transform playerTransform;
    public bool canDrag = true;
    public float offset= 50f;
    public bool isChapter3;
    public float autoMoveSpeed;
    bool initTransitonTrigger;
    float timer = 2f;

    Vector3 GetHandlerScreenPoint => RectTransformUtility.WorldToScreenPoint(null, transform.position);

    private void Start()
    {
        mainCamera = Camera.main;
        //this.SaveHandlerData();
        thisRect = this.GetComponent<RectTransform>();
        TransitionManager.Instance.TriggerAPos = 0;
        TransitionManager.Instance.TriggerBPos = 80;
    }

    private void Update()
    {
        if (isChapter3)
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
                transform.position = new Vector3(1900f, pos.y);
                EventSystem.Instance.EmitEvent(EventName.OnChangeCamera);
            }
            else
                transform.position = new Vector2(pos.x - autoMoveSpeed * Time.deltaTime, pos.y);

            rectTransform.offsetMin = new Vector2(transform.position.x, -(Screen.height / 2));
            this.SaveHandlerData();
        }
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
        rectTransform.offsetMin = new Vector2(pos.x, -(Screen.height / 2));
        RectTransformUtility.ScreenPointToWorldPointInRectangle(thisRect, eventData.position, eventData.enterEventCamera, out pos);
        if (pos.x <= 20)    // 限制线的移动
        {
            transform.position = new Vector3(20f, transform.position.y);
        }
        else if (pos.x >= 1900)
        {
            transform.position = new Vector3(1900f, transform.position.y);
        }
        else
            transform.position = new Vector2(pos.x - offset, transform.position.y);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonType
{
    Jump = 0,
    TwoWaySwitch = 1,
    ChangeScene = 2,
}

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ButtonType buttonType;
    public float pressDurationTime = 0.5f;

    private bool isDown = false;
    private bool isPress = false;
    private float downTime = 0;

    private void Update()
    {
        if (isDown)
        {
            if (isPress) return;
            downTime += Time.deltaTime;
            if (downTime > pressDurationTime)
            {
                isPress = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        switch (buttonType)
        {
            case ButtonType.Jump:
                EventSystem.Instance.EmitEvent(EventName.OnJumpTouch);
                break;

            case ButtonType.TwoWaySwitch:
                EventSystem.Instance.EmitEvent(EventName.OnFunctionTouch, buttonType);
                break;

            case ButtonType.ChangeScene:
                EventSystem.Instance.EmitEvent(EventName.OnFunctionTouch, buttonType);
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        downTime = 0;
        isPress = false;
        if (buttonType == ButtonType.Jump)
        {
            EventSystem.Instance.EmitEvent(EventName.OnStopJumpTouch);
        }
    }
}
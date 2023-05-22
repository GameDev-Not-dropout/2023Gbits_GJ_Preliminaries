using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class MobilePlayerInput : MonoBehaviour
{
    public VariableJoystick joystick;

    [SerializeField] float jumpInputBufferTime = 0.5f;  // 跳跃预输入缓冲时间
    WaitForSeconds waitumpInputBufferTime;

    public float AxisX => isDie ? 0 : joystick.Horizontal;   // 读取输入的水平方向的值
    public bool Move => AxisX != 0;     // 用于判断什么时候开始移动

    public bool HasJumpInputBuffer { get; set; }
    public bool jump;
    public bool stopJump;
    public bool changeScene;
    int chapterIndex;
    bool isDie;

    #region MonoCallBacks

    private void Awake()
    {
        waitumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener(EventName.OnJumpTouch, OnJumpTouch);
        EventSystem.Instance.AddEventListener(EventName.OnStopJumpTouch, OnStopJumpTouch);
        EventSystem.Instance.AddEventListener<ButtonType>(EventName.OnFunctionTouch, OnFunctionTouch);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener(EventName.OnJumpTouch, OnJumpTouch);
        EventSystem.Instance.RemoveEventListener(EventName.OnStopJumpTouch, OnStopJumpTouch);
        EventSystem.Instance.RemoveEventListener<ButtonType>(EventName.OnFunctionTouch, OnFunctionTouch);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            chapterIndex = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            chapterIndex = 2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            chapterIndex = 3;
        }
    }

    #endregion MonoCallBacks

    /// <summary>
    /// 启用动作表
    /// </summary>
    public void EnableGamePlayInput()
    {
        isDie = false;
    }

    public void DisableGamePlayInput()
    {
        isDie = true;
    }

    /// <summary>
    /// 包装跳跃预输入时间协程，供外部调用
    /// </summary>
    public void SetJumpInputBufferTime()
    {
        StopCoroutine(JumpInputBufferCoroutine());
        StartCoroutine(JumpInputBufferCoroutine());
    }

    /// <summary>
    /// 跳跃预输入时间
    /// </summary>
    private IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;
        yield return waitumpInputBufferTime;
        HasJumpInputBuffer = false;
    }

    private void OnJumpTouch()
    {
        stopJump = false;
        jump = true;
    }

    private void OnStopJumpTouch()
    {
        stopJump = true;
        jump = false;
        HasJumpInputBuffer = false; // 只要玩家松开跳跃键，就设置为false，等待玩家下一次的输入
    }

    float lastChangeTime;

    private void OnFunctionTouch(ButtonType buttonType)
    {
        if (buttonType == ButtonType.ChangeScene)
        {
            if (Time.time - lastChangeTime <= 0.5f)   // 切换场景需要0.5s时间间隔
                return;

            lastChangeTime = Time.time;

            EventSystem.Instance.EmitEvent(EventName.OnChangeScene, this.transform);
            if (chapterIndex == 2)
            {
                EventSystem.Instance.EmitEvent(EventName.OnChangeStyle);
            }
        }
    }
}
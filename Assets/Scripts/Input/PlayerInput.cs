using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    [SerializeField] float jumpInputBufferTime = 0.5f;  // 跳跃预输入缓冲时间
    WaitForSeconds waitumpInputBufferTime;

    Vector2 axes => playerInputActions.GamePlay.Axes.ReadValue<Vector2>();  // 读取输入值：只有-1/0/1
    public bool Move => AxisX != 0;     // 用于判断什么时候开始移动
    public float AxisX => axes.x;   // 读取输入的水平方向的值

    public bool HasJumpInputBuffer { get; set; }
    public bool jump => playerInputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool stopJump => playerInputActions.GamePlay.Jump.WasReleasedThisFrame();
    public bool changeScene => playerInputActions.GamePlay.ChangeScene.WasPressedThisFrame();


    #region MonoCallBacks
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        waitumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }
    private void OnEnable()
    {
        // 只要玩家松开跳跃键，就设置为false，等待玩家下一次的输入
        playerInputActions.GamePlay.Jump.canceled += delegate
        {
            HasJumpInputBuffer = false;
        };
    }
    //private void OnGUI()    // 可视化debug
    //{
    //    Rect rect = new Rect(200, 200, 200, 200);
    //    string message = "Has Jump Input Buffer:" + HasJumpInputBuffer;
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 20;
    //    style.fontStyle = FontStyle.Bold;

    //    GUI.Label(rect, message, style);
    //}
    #endregion

    /// <summary>
    /// 启用动作表
    /// </summary>
    public void EnableGamePlayInput()
    {
        playerInputActions.GamePlay.Enable();
    }
    public void DisableGamePlayInput()
    {
        playerInputActions.GamePlay.Disable();
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
    IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;
        yield return waitumpInputBufferTime;
        HasJumpInputBuffer = false;
    }
}

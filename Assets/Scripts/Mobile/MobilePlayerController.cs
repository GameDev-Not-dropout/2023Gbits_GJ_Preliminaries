using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    PlayerGroundDetector groundDetector;
    MobilePlayerInput input;
    [NonSerialized] public Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    public AudioSource VoicePlayer { get; private set; }

    public AudioClip[] runClips;

    public bool Victory { get; private set; }
    public bool CanAirJump { get; set; }
    public bool IsGrounded => groundDetector.IsGrounded;
    public bool IsFalling => rigidBody.velocity.y < 0f && !IsGrounded;
    public float MoveSpeed => Mathf.Abs(rigidBody.velocity.x);  // 玩家水平移动速度要取绝对值

    #region MonoCallBacks

    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        input = GetComponent<MobilePlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        VoicePlayer = GetComponentInChildren<AudioSource>();
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener(EventName.OnJumpUp, SavePlayerJumpPosition);
        EventSystem.Instance.AddEventListener(EventName.OnLand, CompareLandHeight);
        EventSystem.Instance.AddEventListener(EventName.OnSceneFadeEnd, EnableInput);
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, DisableInput);
        EventSystem.Instance.AddEventListener(EventName.OnChangeStyle, ChangeStyle);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener(EventName.OnJumpUp, SavePlayerJumpPosition);
        EventSystem.Instance.RemoveEventListener(EventName.OnLand, CompareLandHeight);
        EventSystem.Instance.RemoveEventListener(EventName.OnSceneFadeEnd, EnableInput);
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, DisableInput);
        EventSystem.Instance.RemoveEventListener(EventName.OnChangeStyle, ChangeStyle);
    }

    private void Start()
    {
        input.EnableGamePlayInput();    // 启用动作表
        LandManager.instance.lastJumpPoint = transform.position;
    }

    private void Update()
    {
        if (groundDetector.IsEnterTransitionTrigger)
        {
            EventSystem.Instance.EmitEvent(EventName.OnTransitionTriggerEnter, transform);
        }
    }

    #endregion MonoCallBacks

    /// <summary>
    /// 根据x轴输入来移动玩家，以及改变玩家朝向
    /// </summary>
    public void Move(float speed)
    {
        if (input.Move)     // 镜像翻转
        {
            transform.localScale = new Vector3(input.AxisX, transform.localScale.y, 1f);
        }

        SetVelocityX(speed * input.AxisX);  // 移动
    }

    #region 方便调整刚体速度

    /// <summary>
    /// 对刚体速度的直接修改
    /// </summary>
    public void SetVelocity(Vector3 velocity) => rigidBody.velocity = velocity;

    /// <summary>
    /// 用于左右移动玩家
    /// </summary>
    public void SetVelocityX(float velocityX)
    {
        rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y);
    }

    /// <summary>
    /// 用于设置Y轴速度
    /// </summary>
    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityY);
    }

    public void AddForceY(float force)
    {
        rigidBody.AddForce(new Vector2(1, force));
    }

    #endregion 方便调整刚体速度

    /// <summary>
    /// 启用/关闭重力
    /// </summary>
    public void SetUseGravity(bool value) => rigidBody.gravityScale = value ? 1 : 0;

    private void OnLevelClear()
    {
        Victory = true;
    }

    private void SavePlayerJumpPosition()
    {
        LandManager.instance.lastJumpPoint = transform.position;
    }

    private void CompareLandHeight()
    {
        if (LandManager.instance.lastJumpPoint.y - transform.position.y >= 8)
        {
            // 角色死亡
            //SceneFadeManager.Instance.ReSetPlayerPosition();
            EventSystem.Instance.EmitEvent(EventName.OnPlayerDie, transform);
            return;
        }
        LandManager.instance.lastJumpPoint = transform.position;
    }

    private void DisableInput(Transform transform)
    {
        input.DisableGamePlayInput();
    }

    private void EnableInput()
    {
        input.EnableGamePlayInput();
    }

    float lastChangeTime;

    private void ChangeStyle()
    {
        if (Time.time - lastChangeTime <= 0.5f)   // 切换场景需要0.5s时间间隔
            return;

        lastChangeTime = Time.time;

        if (boxCollider.size.x == 1)
        {
            boxCollider.size = new Vector2(1.4f, 0.85f);   // 变成猫的尺寸
            VoicePlayer.clip = runClips[1];
        }
        else
        {
            boxCollider.size = new Vector2(1f, 2f);
            VoicePlayer.clip = runClips[0];
        }
    }
}
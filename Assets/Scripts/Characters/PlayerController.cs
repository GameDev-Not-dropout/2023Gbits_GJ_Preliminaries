using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //[SerializeField] VoidEventChannel levelClearedEventChannel;

    PlayerGroundDetector groundDetector;
    PlayerInput input;
    Rigidbody2D rigidBody;
    public AudioSource VoicePlayer { get; private set; }

    public bool Victory { get; private set; }
    public bool CanAirJump { get; set; }
    public bool IsGrounded => groundDetector.IsGrounded;
    public bool IsFalling => rigidBody.velocity.y < 0f && !IsGrounded;
    public float MoveSpeed => Mathf.Abs(rigidBody.velocity.x);  // 玩家水平移动速度要取绝对值

    #region MonoCallBacks
    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        VoicePlayer = GetComponentInChildren<AudioSource>();
    }
    private void OnEnable()
    {
        //levelClearedEventChannel.AddListener(OnLevelClear);
    }
    private void Start()
    {
        input.EnableGamePlayInput();    // 启用动作表
    }
    private void OnDisable()
    {
        //levelClearedEventChannel.RemoveListener(OnLevelClear);
    }
    #endregion

    /// <summary>
    /// 根据x轴输入来移动玩家，以及改变玩家朝向
    /// </summary>
    public void Move(float speed)
    {
        if (input.Move)     // 镜像翻转
        {
            transform.localScale = new Vector3(input.AxisX, 1f, 1f);
        }
        SetVelocityX(speed * input.AxisX);

        if (transform.position.x < 10)      // 玩家在场景1
        {
            if (transform.position.x > TransitionManager.Instance.handler2)    // 跳转到场景2
            {
                transform.position = new Vector3(transform.position.x + 20, transform.position.y);
            }
        }
        if (transform.position.x > 10 && transform.position.x < 30)    // 玩家在场景2
        {
            if (transform.position.x - 20 > TransitionManager.Instance.handler3)    // 跳转到场景3
            {
                transform.position = new Vector3(transform.position.x + 20, transform.position.y);
            }
            if (transform.position.x - 20 < TransitionManager.Instance.handler2)    // 回到场景1
            {
                transform.position = new Vector3(transform.position.x - 20, transform.position.y);
            }
        }
        if (transform.position.x > 30)    // 玩家在场景3
        {
            if (transform.position.x - 40 < TransitionManager.Instance.handler3)    // 回到场景2
            {
                transform.position = new Vector3(transform.position.x - 20, transform.position.y);
            }
        }
    }

    #region 方便调整刚体速度
    /// <summary>
    /// 对刚体速度的直接修改
    /// </summary>
    public void SetVelocity(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

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
    #endregion


    /// <summary>
    /// 启用/关闭重力
    /// </summary>
    public void SetUseGravity(bool value)
    {
        if (value)
        {
            rigidBody.gravityScale = 1;

        }
        else
        {
            rigidBody.gravityScale = 0;
        }
    }

    void OnLevelClear()
    {
        Victory = true;
    }
}

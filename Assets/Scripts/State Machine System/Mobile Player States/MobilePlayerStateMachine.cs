using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobilePlayerStateMachine : StateMachine  // 挂载到玩家身上
{
    [SerializeField] MobilePlayerState[] states;
    public SpriteRenderer copyImage;
    public RuntimeAnimatorController playerCtr;
    public RuntimeAnimatorController catCtr;

    Animator animator;
    MobilePlayerController player;
    MobilePlayerInput input;
    SpriteRenderer spriteRenderer;
    float lastChangeTime;

    private void Awake()
    {
        #region 状态需要绑定的组件

        animator = GetComponentInChildren<Animator>();
        player = GetComponent<MobilePlayerController>();
        input = GetComponent<MobilePlayerInput>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        #endregion 状态需要绑定的组件

        stateTable = new Dictionary<System.Type, IState>(states.Length);    // 初始化状态字典

        foreach (MobilePlayerState state in states)
        {
            state.Initialize(animator, player, input, this);    // 为每个具体状态绑定组件（动画、输入控制器、输入和状态机）
            stateTable.Add(state.GetType(), state);         // 为状态字典填充内容
        }
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener(EventName.OnChangeStyle, ChangeStyle);
        //EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, OnPlayerDie);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener(EventName.OnChangeStyle, ChangeStyle);
        //EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, OnPlayerDie);
    }

    private void OnPlayerDie(Transform trans)
    {
        SwitchState(typeof(MobilePlayerState_Die));
    }

    private void Start()
    {
        SwitchOn(stateTable[typeof(MobilePlayerState_Idle)]);    // 状态机以空闲状态启动
    }

    public override void TimeUpdate()
    {
        base.TimeUpdate();
        if (copyImage != null)
            copyImage.sprite = spriteRenderer.sprite;

        Vector3 pos = transform.position;
        if (pos.x < 40)
        {
            if (transform.localScale.x < 0)
                copyImage.transform.localPosition = new Vector3(-80, 0);
            else
                copyImage.transform.localPosition = new Vector3(80, 0);
        }
        else
        {
            if (transform.localScale.x < 0)
                copyImage.transform.localPosition = new Vector3(80, 0);
            else
                copyImage.transform.localPosition = new Vector3(-80, 0);
        }
    }

    private void ChangeStyle()
    {
        if (Time.time - lastChangeTime <= 0.5f)   // 切换场景需要0.5s时间间隔
            return;

        lastChangeTime = Time.time;

        if (animator.runtimeAnimatorController.name == playerCtr.name)
        {
            animator.runtimeAnimatorController = catCtr;    // 变成猫
        }
        else
        {
            animator.runtimeAnimatorController = playerCtr;    // 变成人
            if (stateTable[currentState.GetType()] is MobilePlayerState_Run)
            {
                animator.Play("Run");
                player.VoicePlayer.clip = player.runClips[0];
                player.VoicePlayer.Play();
            }
        }

        if (transform.position.x < 40)
        {
            TransitionManager.Instance.SpawnShockWaves(transform.position, 1);
            Vector3 pos = new Vector3(transform.position.x + 80f, transform.position.y);
            TransitionManager.Instance.SpawnShockWaves(pos, 2);
        }
        else
        {
            TransitionManager.Instance.SpawnShockWaves(transform.position, 2);
            Vector3 pos = new Vector3(transform.position.x - 80f, transform.position.y);
            TransitionManager.Instance.SpawnShockWaves(pos, 1);
        }
    }
}
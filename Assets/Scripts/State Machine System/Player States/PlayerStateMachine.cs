using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine  // 挂载到玩家身上
{
    [SerializeField] PlayerState[] states;  

    Animator animator;
    PlayerController player;
    PlayerInput input;

    private void Awake()
    {
        #region 状态需要绑定的组件
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<PlayerController>();
        input = GetComponent<PlayerInput>(); 
        #endregion

        stateTable = new Dictionary<System.Type, IState>(states.Length);    // 初始化状态字典

        foreach (PlayerState state in states)
        {
            state.Initialize(animator, player, input, this);    // 为每个具体状态绑定组件（动画、输入控制器、输入和状态机）
            stateTable.Add(state.GetType(), state);         // 为状态字典填充内容
        }
    }
    private void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);    // 状态机以空闲状态启动
    }
}

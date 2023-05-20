using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机类的主要功能有两点
/// 1.持有所有的状态类，并对它们进行管理和切换
/// 2.负责进行当前状态的更新
/// </summary>
public class StateMachine : TimeControlled
{
    protected IState currentState;

    protected Dictionary<System.Type, IState> stateTable;   // 以状态的类型为key来构建字典，保证集合中的每个状态都是唯一的

    #region 状态的更新

    public override void TimeUpdate()
    {
        base.TimeUpdate();
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    #endregion 状态的更新

    #region 启动状态

    /// <summary>
    /// 启动当前状态，给子类使用
    /// </summary>
    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// 切换状态，核心方法
    /// </summary>
    private void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }

    /// <summary>
    /// 状态切换重载方法，参数为type类型，供外部使用
    /// </summary>
    public void SwitchState(System.Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }

    #endregion 启动状态
}
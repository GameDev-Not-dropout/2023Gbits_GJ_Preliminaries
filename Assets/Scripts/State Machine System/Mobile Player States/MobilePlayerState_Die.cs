using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(menuName = "Data/StateMachine/MobilePlayerState/Die", fileName = "MobilePlayerState_Die")]
public class MobilePlayerState_Die : MobilePlayerState
{
    public float exitTime = 1.5f;
    float timer;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        #region 状态切换

        if (timer >= exitTime)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Idle));
        }
        else
        {
            timer += Time.deltaTime;
        }

        #endregion 状态切换
    }

    public override void PhysicUpdate()
    {
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Die", fileName = "PlayerState_Die")]	
public class PlayerState_Die : PlayerState
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
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        else
        {
            timer += Time.deltaTime;
        }

        #endregion
    }

    public override void PhysicUpdate()
    {
        
    }










}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/MobilePlayerState/CoyoteTime", fileName = "MobilePlayerState_CoyoteTime")]
public class MobilePlayerState_CoyoteTime : MobilePlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float coyoteTime = 0.1f;

    public override void Enter()
    {
        base.Enter();

        player.SetUseGravity(false);    // 关闭重力
    }

    public override void Exit()
    {
        player.SetUseGravity(true);     // 启用重力
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        #region 状态切换

        if (input.jump)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_JumpUp));
        }
        if (StateDuration > coyoteTime || !input.Move)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Fall));
        }

        #endregion 状态切换
    }

    public override void PhysicUpdate()
    {
        player.Move(runSpeed);
    }
}
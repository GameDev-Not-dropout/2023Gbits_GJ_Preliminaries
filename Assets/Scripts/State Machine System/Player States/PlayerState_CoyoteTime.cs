using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
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
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        if (StateDuration > coyoteTime || !input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        #endregion

    }

    public override void PhysicUpdate()
    {
        player.Move(runSpeed);
    }
}

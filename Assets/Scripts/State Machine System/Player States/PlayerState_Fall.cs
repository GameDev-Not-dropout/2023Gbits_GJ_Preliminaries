using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;     // 动画曲线
    [SerializeField] float moveSpeed = 5f;
    public override void LogicUpdate()
    {
        #region 状态切换
        if (player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }
        if (input.jump)
        {
            if (player.CanAirJump)
            {
                stateMachine.SwitchState(typeof(PlayerState_AirJump));
                return;
            }
            input.SetJumpInputBufferTime();    // 如果玩家没有二段跳功能就可以进行跳跃预输入
        } 
        #endregion
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));  // 取得曲线上某个时间点的值，实现掉落速度的完全可控
        player.Move(moveSpeed);     // 玩家落下时，仍然可以进行水平方向的位移
    }
}

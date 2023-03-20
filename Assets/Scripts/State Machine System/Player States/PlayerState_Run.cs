using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float acceleration = 15f;   // 玩家速度逐帧的加速值

    public override void Enter()
    {
        base.Enter();

        currentSpeed = player.MoveSpeed;    // 记录当前玩家速度的值
    }
    public override void LogicUpdate()
    {
        #region 状态切换
        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (input.jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        if (!player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
        }
        #endregion

        currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.deltaTime); // 逐帧修改玩家速度的值到达最大值
    }

    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}

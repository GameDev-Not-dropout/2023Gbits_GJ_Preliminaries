using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/MobilePlayerState/MobileIdle", fileName = "MobilePlayerState_Idle")]
public class MobilePlayerState_Idle : MobilePlayerState
{
    [SerializeField] float deceleration = 20f;   // 玩家速度逐帧的减速值

    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;    // 记录当前玩家速度的值
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        #region 状态切换

        if (input.Move)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Run));
        }
        if (input.jump)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_JumpUp));
        }
        if (!player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Fall));
        }

        #endregion 状态切换

        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        // 由于减速过程中玩家没有输入，因此不使用Move方法，而是直接使用SetVelocityX方法
        player.SetVelocityX(currentSpeed * player.transform.localScale.x);  // 向玩家朝向方向进行减速移动
    }
}
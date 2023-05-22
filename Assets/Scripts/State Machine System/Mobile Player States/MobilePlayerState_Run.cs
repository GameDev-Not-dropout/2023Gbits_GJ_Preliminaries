using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/MobilePlayerState/MobileRun", fileName = "MobilePlayerState_Run")]
public class MobilePlayerState_Run : MobilePlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float acceleration = 15f;   // 玩家速度逐帧的加速值

    public override void Enter()
    {
        base.Enter();

        currentSpeed = player.MoveSpeed;    // 记录当前玩家速度的值
        player.VoicePlayer.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        #region 状态切换

        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Idle));
            player.VoicePlayer.Stop();
        }
        if (input.jump)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_JumpUp));
            player.VoicePlayer.Stop();
        }
        if (!player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_CoyoteTime));
            player.VoicePlayer.Stop();
        }

        #endregion 状态切换

        currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.deltaTime); // 逐帧修改玩家速度的值到达最大值
    }

    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}
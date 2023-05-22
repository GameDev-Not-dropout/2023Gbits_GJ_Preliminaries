using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/MobilePlayerState/MobileJumpUp", fileName = "MobilePlayerState_JumpUp")]
public class MobilePlayerState_JumpUp : MobilePlayerState
{
    [SerializeField] AnimationCurve speedCurve;     // 动画曲线

    //[SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;

    //[SerializeField] ParticleSystem jumpVFX;
    //[SerializeField] AudioClip jumpSFX;

    public override void Enter()
    {
        base.Enter();

        input.HasJumpInputBuffer = false;

        SoundManager.Instance.PlaySound(SE.jump);
        EventSystem.Instance.EmitEvent(EventName.OnJumpUp);
        //player.AddForceY(jumpForce);     // 给玩家向上施加力，实现跳跃效果
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.stopJump || player.IsFalling)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
        player.Move(moveSpeed);     // 玩家跳起时，仍然可以控制水平位移
    }
}
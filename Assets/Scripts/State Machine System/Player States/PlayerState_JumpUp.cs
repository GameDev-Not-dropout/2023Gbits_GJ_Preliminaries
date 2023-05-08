using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/JumpUp", fileName = "PlayerState_JumpUp")]
public class PlayerState_JumpUp : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;     // 动画曲线
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;
    //[SerializeField] ParticleSystem jumpVFX;
    //[SerializeField] AudioClip jumpSFX;

    public override void Enter()
    {
        base.Enter();

        input.HasJumpInputBuffer = false;
        EventSystem.instance.EmitEvent(EventName.OnJumpUp);
        //player.AddForceY(jumpForce);     // 给玩家向上施加力，实现跳跃效果

        //player.VoicePlayer.PlayOneShot(jumpSFX);
        //Instantiate(jumpVFX, player.transform.position, Quaternion.identity);   // 播放粒子特效
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.stopJump || player.IsFalling)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
        player.Move(moveSpeed);     // 玩家跳起时，仍然可以控制水平位移
    }
}

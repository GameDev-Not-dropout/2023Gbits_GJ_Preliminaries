
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/AirJump", fileName = "PlayerState_AirJump")]
public class PlayerState_AirJump : PlayerState
{
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] ParticleSystem jumpVFX;
    [SerializeField] AudioClip jumpSFX;

    public override void Enter()
    {
        base.Enter();

        player.CanAirJump = false;
        player.SetVelocityY(jumpForce);     // 给玩家向上施加力，实现跳跃效果

        player.VoicePlayer.PlayOneShot(jumpSFX);
        Instantiate(jumpVFX, player.transform.position, Quaternion.identity);   // 播放粒子特效
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
        player.Move(moveSpeed);     // 玩家跳起时，仍然可以控制水平位移
    }
}

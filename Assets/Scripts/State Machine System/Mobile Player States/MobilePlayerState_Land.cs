using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/MobilePlayerState/MobileLand", fileName = "MobilePlayerState_Land")]
public class MobilePlayerState_Land : MobilePlayerState
{
    [SerializeField] float stiffTime = 0.2f;    // 硬直时间

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySound(SE.land);
        EventSystem.Instance.EmitEvent(EventName.OnLand);
        player.SetVelocity(Vector3.zero);   // 落地时将玩家刚体速度归零
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        #region 状态切换

        if (input.HasJumpInputBuffer || input.jump)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_JumpUp));
        }
        if (StateDuration < stiffTime)  // 当前状态下的动画持续时间＜硬直时间，则不执行下面的代码
        {
            return;
        }
        if (input.Move)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Run));
        }
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MobilePlayerState_Idle));
        }

        #endregion 状态切换
    }
}
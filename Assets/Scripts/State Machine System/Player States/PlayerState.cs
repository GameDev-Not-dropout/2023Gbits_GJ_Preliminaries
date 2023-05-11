
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] string stateName;
    [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;    // 淡化时间
    int stateHash;
    float stateStarTime;

    protected float currentSpeed;   // 用来存储玩家当前的速度值

    protected Animator animator;
    protected PlayerController player;
    protected PlayerInput input;
    protected PlayerStateMachine stateMachine;    // 用来进行状态的切换

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStarTime;     // 当前状态下的动画持续时间

    private void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    public void Initialize(Animator animator, PlayerController player, PlayerInput input, PlayerStateMachine stateMachine)
    {
        this.animator = animator;
        this.player = player;
        this.input = input;
        this.stateMachine = stateMachine;
    }


    #region 玩家状态，留给子类重写
    public virtual void Enter()
    {
        animator.CrossFade(stateHash, transitionDuration);
        stateStarTime = Time.time;
    }       
            
    public virtual void Exit()
    {       
            
    }       
            
    public virtual void LogicUpdate()
    {
        if (input.changeScene)
        {
            EventSystem.Instance.EmitEvent(EventName.OnChangeScene, stateMachine.transform);
        }
    }       
            
    public virtual void PhysicUpdate()
    {

    } 
    #endregion


}

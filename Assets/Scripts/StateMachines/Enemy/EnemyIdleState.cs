using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LOCOMOTIONHASH = Animator.StringToHash("Locomotion");
    private readonly int SPEEDHASH = Animator.StringToHash("Speed");

    private const float crossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(LOCOMOTIONHASH, crossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            Debug.Log("in range");
            enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));
            return;
        }

        FacePlayer();

        enemyStateMachine.Animator.SetFloat(SPEEDHASH, 0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }

}

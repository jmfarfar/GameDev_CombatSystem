using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int IMPACTHASH = Animator.StringToHash("Impact");

    private const float CROSSFADEDURATION = 0.1f;
    private float duration = 1f;

    public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(IMPACTHASH, CROSSFADEDURATION);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
        }
    }

    public override void Exit()
    {
        
    }

}

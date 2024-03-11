using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int ATTACKHASH = Animator.StringToHash("Attack");

    private const float crossFadeDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {

        enemyStateMachine.Weapon.SetAttack(enemyStateMachine.AttackDamage, enemyStateMachine.AttackKnockback);

        enemyStateMachine.Animator.CrossFadeInFixedTime(ATTACKHASH, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(enemyStateMachine.Animator) >= 1)
        {
            enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));

        }
    }

    public override void Exit()
    {

    }

}

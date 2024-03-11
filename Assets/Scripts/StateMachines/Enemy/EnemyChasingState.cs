using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LOCOMOTIONHASH = Animator.StringToHash("Locomotion");
    private readonly int SPEEDHASH = Animator.StringToHash("Speed");

    private const float crossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(LOCOMOTIONHASH, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {

        if (!IsInChaseRange())
        {
            Debug.Log("not in chase range");
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyAttackingState(enemyStateMachine));
            return;
        }

        MoveToPlayer(deltaTime);

        FacePlayer();

        enemyStateMachine.Animator.SetFloat(SPEEDHASH, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        enemyStateMachine.Agent.ResetPath();
        enemyStateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        enemyStateMachine.Agent.destination = enemyStateMachine.Player.transform.position;

        Move(enemyStateMachine.Agent.desiredVelocity.normalized * enemyStateMachine.MovementSpeed, deltaTime);

        enemyStateMachine.Agent.velocity = enemyStateMachine.CharacterController.velocity;
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= enemyStateMachine.AttackRange * enemyStateMachine.AttackRange;
    }

}

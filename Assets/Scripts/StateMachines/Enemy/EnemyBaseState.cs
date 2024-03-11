using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public abstract class EnemyBaseState : State
{

    protected EnemyStateMachine enemyStateMachine;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }

    protected bool IsInChaseRange()
    {
        float playerDistanceSqr = (enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= enemyStateMachine.PlayerChasingRange * enemyStateMachine.PlayerChasingRange;
    }    

    protected void FacePlayer()
    {
        if (enemyStateMachine.Player == null)
        {
            return;
        }

        var lookPos = enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position;
        lookPos.y = 0f;

        enemyStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        enemyStateMachine.CharacterController.Move((motion + enemyStateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    private GameObject FollowTarget;
    private float AttackRange = 1.5f;

    public ZombieAttackState(GameObject followTarget, ZombieComponent zombie, StateMachine statemachine) : base(zombie, statemachine)
    {
        FollowTarget = followTarget;
        UpdateInterval = 2.0f; //this will be our attack interval
    }

    public override void Start()
    {
        base.Start();
        OwnerZombie.ZombieNavMesh.isStopped = true;
        OwnerZombie.ZombieNavMesh.ResetPath();
        OwnerZombie.ZombieAnimator.SetFloat("MovementZ", 0.0f);

        OwnerZombie.ZombieAnimator.SetBool("IsAttacking", true);

    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        //TODO: Add damage to the object
    }

    public override void Update()
    {
        base.Update();
        OwnerZombie.transform.LookAt(FollowTarget.transform, Vector3.up);

        float distanceBetween = Vector3.Distance(OwnerZombie.transform.position, FollowTarget.transform.position);

        if (distanceBetween > AttackRange)
        {
            StateMachine.ChangeState(ZombieStateType.Follow);
        }

        //TODO zombie health < 0 die
    }

    public override void Exit()
    {
        base.Exit();

        OwnerZombie.ZombieAnimator.SetBool("IsAttacking", false);
    }
}

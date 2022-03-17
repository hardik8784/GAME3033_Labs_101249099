using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieStates
{
    GameObject followTarget;
    float stoppingDistance = 2;
    int MovementZHash = Animator.StringToHash("MovementZ");

    public ZombieFollowState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        updateInterval = 2f;
    }

    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavmeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        ownerZombie.zombieNavmeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void Update()
    {
        base.Update();
        float moveZ = ownerZombie.zombieNavmeshAgent.velocity.normalized.z != 0 ? 1.0f : 0.0f;
        ownerZombie.zombieAnimator.SetFloat(MovementZHash, moveZ);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween < stoppingDistance)
        {
            //Change State to Following here
            stateMachine.ChangeState(ZombieStateType.Attacking);
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : ZombieStates
{
    int MovementZHash = Animator.StringToHash("MovementZ");
    int isDeadHash = Animator.StringToHash("isDead");
    public ZombieDeadState(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        updateInterval = 0;
    }

    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavmeshAgent.isStopped = true;
        ownerZombie.zombieNavmeshAgent.ResetPath();
        
        ownerZombie.zombieAnimator.SetFloat(MovementZHash, 0);
        ownerZombie.zombieAnimator.SetBool(isDeadHash, true);
    }


    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavmeshAgent.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isDeadHash, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    public int zombieDamage = 5;

    public NavMeshAgent zombieNavmeshAgent;

    public Animator zombieAnimator;

    public ZombieStateMachine zombieStateMachine;

    public GameObject followTarget;

    private void Awake()
    {
        zombieNavmeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        Initialize(followTarget);
    }

    public void Initialize(GameObject _followTarget)
    {
        //followTarget = _followTarget;
    }
}

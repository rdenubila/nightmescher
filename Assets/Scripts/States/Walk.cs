using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walk : IState
{
    private readonly Player _player;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;

    public Walk(Player player, NavMeshAgent navMeshAgent, Animator animator)
    {
        _player = player;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick() {
        _animator.SetFloat("Velocity", _navMeshAgent.velocity.magnitude);
    }

    public void OnEnter() {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_player.GetNextDestination());
    }

    public void OnExit() {
        _navMeshAgent.enabled = false;
        _animator.SetFloat("Velocity", 0);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : IState
{
    private readonly Player _player;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;

    public Idle(Player player, NavMeshAgent navMeshAgent, Animator animator)
    {
        _player = player;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick() { }

    public void OnEnter() {
        _navMeshAgent.enabled = false;
        _animator.SetFloat("Velocity", 0);
    }

    public void OnExit() { }
}

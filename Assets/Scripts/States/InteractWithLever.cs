using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractWithLever : IState
{
    private readonly Player _player;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private Lever _lever;

    public InteractWithLever(Player player, NavMeshAgent navMeshAgent, Animator animator)
    {
        _player = player;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick() {
    }

    public void OnEnter() {
        _lever = _player.GetCurrentLever();
        _lever.Interact();

        LeanTween.rotate(_player.gameObject, _lever.playerPos.rotation.eulerAngles, .25f);
        LeanTween.move(_player.gameObject, _lever.playerPos.position, .25f);
        _player.SetRightHandObj(_lever.playerHandPos);

        GameObject.FindObjectOfType<GameController>().ShowGeneralCamera(2f, .5f);

        LeanTween.value(0f, 1f, 1f).setOnComplete(()=> { _player.InteractLever(null); });

    }



    public void OnExit() {
        _player.SetRightHandObj(null);

    }
}
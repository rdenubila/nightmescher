using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : IState
{
    private readonly Player _player;

    public EnterDoor(Player player) {
        _player = player;
    }

    public void Tick() { }

    public void OnEnter() {
        Door d = _player.GetDoor();
        _player.transform.position = d.destination.insidePos.position;
        _player.transform.rotation = d.destination.insidePos.rotation;
        _player.SetDestination(d.destination.frontPos.position);
    }

    public void OnExit() {
        _player.SetDoor(null);
    }
}
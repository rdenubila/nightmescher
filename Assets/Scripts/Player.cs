using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    private StateMachine _stateMachine;

    Vector3 _currentDestination = Vector3.zero;
    Vector3 _nextDestination = Vector3.zero;
    Lever _currentLever;
    Door _currentDoor;
    Transform rightHandObj;
    Animator animator;
    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _stateMachine = new StateMachine();

        var idle = new Idle(this, navMeshAgent, animator);
        var walk = new Walk(this, navMeshAgent, animator);
        var changeDestination = new ChangeDestination(this);
        var enterDoor = new EnterDoor(this);
        var interactLever = new InteractWithLever(this, navMeshAgent, animator);

        At(walk, interactLever, InteractLever());
        At(interactLever, idle, ()=> _currentLever == null );

        At(walk, enterDoor, EnterDoor());
        At(enterDoor, walk, () => true);


        At(idle, walk, ()=> _nextDestination != Vector3.zero );
        At(walk, idle, ()=> Vector3.Distance(transform.position, _currentDestination) < .1f );


        At(walk, changeDestination, ()=> _nextDestination != Vector3.zero );
        At(changeDestination, walk, ()=> true );


        _stateMachine.SetState(idle);




        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        Func<bool> InteractLever() => () => Vector3.Distance(transform.position, _currentDestination) < .2f && _currentLever !=null;
        Func<bool> EnterDoor() => () => Vector3.Distance(transform.position, _currentDestination) < .15f && _currentDoor != null;

    }

    public void SetDestination(Vector3 _dest)
    {
        _nextDestination = _dest;
    }

    public void InteractLever(Lever lever)
    {
        _currentLever = lever;
        if(_currentLever) SetDestination(_currentLever.playerPos.position);
    }

    public void SetDoor(Door door)
    {
        _currentDoor = door;
        if(_currentDoor) SetDestination(_currentDoor.insidePos.position);
    }

    public Door GetDoor()
    {
        return _currentDoor;
    }

    public void SetRightHandObj(Transform obj) => rightHandObj = obj;

    public Vector3 GetNextDestination()
    {
        _currentDestination = _nextDestination;
        _nextDestination = Vector3.zero;
        return _currentDestination;
    }

    public Lever GetCurrentLever()
    {
        Lever l = _currentLever;
        return l;
    }

    public void ClearInteractions()
    {
        _currentDoor = null;
        _currentLever = null;
    }

    private void Update ()=>_stateMachine.Tick();

    void OnAnimatorIK()
    {
        if (animator)
        {

            GameObject lookAtObj = GameObject.FindGameObjectsWithTag("Lever").Where((obj) => Vector3.Distance(obj.transform.position, transform.position) < 3f).FirstOrDefault();
            if (lookAtObj != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookAtObj.transform.position + lookAtObj.transform.up* 1.35f);
            } else
            {
                animator.SetLookAtWeight(0);
            }



            // Set the right hand target position and rotation, if one has been assigned
            if (rightHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                //animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            } else {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }

}

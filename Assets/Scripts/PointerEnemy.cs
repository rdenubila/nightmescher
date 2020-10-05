using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerEnemy : MonoBehaviour
{

    Player _player;
    public float distance = 3f;
    Animator animator;
    bool sawPlayer = false;
    Vector3 up;
    Quaternion initialRot;
    Quaternion targetRotation;

    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        up = transform.up;
        initialRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        print(RoundV3(transform.up) + " - " + RoundV3(_player.transform.up) + " = "+ (RoundV3(transform.up) == RoundV3(_player.transform.up)));
        print(transform.up.x);

        if (
            Vector3.Distance(transform.position, _player.transform.position) < distance
            && RoundV3(transform.up) == RoundV3(_player.transform.up)
        )
        {
            animator.SetBool("Laugh", true);

            Vector3 targetPoint = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, up);

            if (!sawPlayer)
            {
                sawPlayer = true;
                GameObject.FindObjectOfType<Mission_3>().Collect();
            }
            

        } else
        {
            animator.SetBool("Laugh", false);
            targetRotation = initialRot;

        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .2f);

    }

    Vector3 RoundV3(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }

    float lastWeight;
    void OnAnimatorIK()
    {
        if (animator)
        {

            Vector3 pos = _player.transform.position + _player.transform.up * 1.35f;

            animator.SetLookAtPosition(pos);
            animator.SetIKPosition(AvatarIKGoal.RightHand, pos);
            
            float destWeight = 0;
            if(animator.GetBool("Laugh")) destWeight = 1; else destWeight = 0;
            lastWeight = Mathf.Lerp(lastWeight, destWeight, .1f);
            animator.SetLookAtWeight(lastWeight);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, lastWeight);

            /*
            GameObject lookAtObj = GameObject.FindGameObjectsWithTag("Lever").Where((obj) => Vector3.Distance(obj.transform.position, transform.position) < 3f).FirstOrDefault();
            if (lookAtObj != null)
            {
                animator.SetLookAtWeight(1);
                
            }
            else
            {
                animator.SetLookAtWeight(0);
            }



            // Set the right hand target position and rotation, if one has been assigned
            if (rightHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                //animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
            */
        }
    }
}

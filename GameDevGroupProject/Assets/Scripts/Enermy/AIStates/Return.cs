using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return<T> : AIState<T>
{
    public Return(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }
    

    public override void OnEnter()
    {
        brain.returning = true;
        brain.navMeshAgent.SetDestination(brain.origin.GetComponent<Transform>().position);
        brain.animator.SetBool("Moving", true);
        base.OnEnter();
    }

    public override void OnLeave()
    {
        brain.animator.SetBool("Moving", false);
        brain.transform.rotation = (brain.startRotation);
        base.OnLeave();
    }

    public override void OnStateTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.tag == brain.origin.tag)
        {
            Debug.Log("back at origin");
            brain.returning = false;
        }
    }
}
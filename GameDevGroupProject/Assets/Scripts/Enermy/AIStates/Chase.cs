using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase<T> : AIState<T>
{
    public Chase(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void Act()
    {
        brain.navMeshAgent.SetDestination(brain.Player.GetComponent<Transform>().position);
    }

    public override void OnEnter()
    {
        brain.chasing = true;
        brain.animator.SetBool("Moving", true);
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}

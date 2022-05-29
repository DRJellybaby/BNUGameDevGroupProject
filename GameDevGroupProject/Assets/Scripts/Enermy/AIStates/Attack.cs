using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack<T> : AIState<T>
{
    public Attack(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void Act()
    {
        
    }

    public override void OnEnter()
    {
        brain.navMeshAgent.isStopped = true;
        brain.navMeshAgent.velocity = Vector3.zero;
        brain.animator.SetBool("Moving", false);
        brain.attack();
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }


}

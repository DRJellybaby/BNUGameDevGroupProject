using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea<T> : AIState<T>
{
    public SearchArea(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

    private Vector3 destination;

    public override void OnEnter()
    {
        brain.wait = true;
        brain.navMeshAgent.isStopped = true;
        brain.animator.SetBool("Moving", false);
        brain.startTimer();
        base.OnEnter();
    }

    public override void OnLeave()
    {
        brain.navMeshAgent.isStopped = false;
        base.OnLeave();
    }
}
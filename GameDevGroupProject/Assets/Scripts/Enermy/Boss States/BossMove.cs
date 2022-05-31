using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove<T> : BossAIStates<T>
{
    public BossMove(T stateName, BossBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void Act()
    {
        brain.navMeshAgent.SetDestination(brain.Player.GetComponent<Transform>().position);
    }

    public override void OnEnter()
    {
        brain.navMeshAgent.isStopped = false;
        brain.animator.SetBool("Moving", true);
        base.OnEnter();
    }

    public override void OnLeave()
    {
        brain.animator.SetBool("Moving", false);
        base.OnLeave();
    }


}
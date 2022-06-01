using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack <T> : BossAIStates<T>
{
    public BossAttack(T stateName, BossBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void Act() {}

    public override void OnEnter()
    {
        Debug.Log("Distance: " + brain.distanceToTarget + "|" + "Attack Range: " + brain.attackRange);
        brain.navMeshAgent.isStopped = true;
        brain.navMeshAgent.velocity = Vector3.zero;
        brain.attack();
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}

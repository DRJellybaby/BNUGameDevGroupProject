using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack <T> : BossAIStates<T>
{
    public BossAttack(T stateName, BossBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


    public override void Act() { brain.navMeshAgent.isStopped = true; }

    public override void OnEnter()
    {
        brain.navMeshAgent.isStopped = true;
        brain.navMeshAgent.velocity = Vector3.zero;
        Debug.Log("Distance: " + brain.distanceToPlayer + "|" + "Attack Range: " + brain.attackRange);
        brain.attack();
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}

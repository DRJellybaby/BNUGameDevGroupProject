using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle<T> : AIState<T>
{ 
    public Idle(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

    public override void Act()
    {
      
    }

    public override void OnEnter()
    {
        brain.navMeshAgent.ResetPath();
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}

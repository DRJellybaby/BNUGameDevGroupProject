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
        base.OnEnter();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}

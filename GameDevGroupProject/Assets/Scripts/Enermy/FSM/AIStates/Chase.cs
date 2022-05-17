using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase<T> : AIState<T>
{
    public Chase(T stateName, StateDriveBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

    public override void Act()
    {
        Debug.Log("Entering state: " + StateName);
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

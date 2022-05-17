using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search<T> : AIState<T>
{
    public Search(T stateName, StateDriveBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

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

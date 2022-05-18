using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea<T> : AIState<T>
{
    public SearchArea(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

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

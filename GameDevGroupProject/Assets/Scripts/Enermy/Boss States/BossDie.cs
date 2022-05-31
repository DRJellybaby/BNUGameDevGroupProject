using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie<T> : BossAIStates<T>
{
    public BossDie(T stateName, BossBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


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

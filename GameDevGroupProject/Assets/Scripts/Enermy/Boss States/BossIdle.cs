using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle<T> : BossAIStates<T>
{
    public BossIdle(T stateName, BossBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


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


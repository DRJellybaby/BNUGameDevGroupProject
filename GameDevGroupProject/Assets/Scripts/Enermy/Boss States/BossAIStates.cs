using UnityEngine;
using System.Collections;

public class BossAIStates<T> : State<T> {
    protected BossBrain brain;
    protected Vector3 moveTarget;
    protected Vector3 moveDirection;
    protected Vector3 moveRotation;
    protected Quaternion lookAtRotation;

    public BossAIStates(T stateName, BossBrain brain, float minDuration): base(stateName, brain, minDuration) {
        this.brain = brain;
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
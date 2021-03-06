using UnityEngine;
using System.Collections;

public class AIState<T> : State<T> {
    protected StateDrivenBrain brain;
    protected Vector3 moveTarget;
    protected Vector3 moveDirection;
    protected Vector3 moveRotation;
    protected Quaternion lookAtRotation;



    public AIState(T stateName, StateDrivenBrain brain, float minDuration): base(stateName, brain, minDuration) {
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

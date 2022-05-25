using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase<T> : AIState<T>
{
    public Chase(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

    private Transform lastSeenLocation;

    public override void OnEnter()
    {
        base.OnEnter();
        brain.chasing = true;
        lastSeenLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        brain.chasePoint.GetComponent<Transform>().position = new Vector3(lastSeenLocation.position.x, lastSeenLocation.position.y, lastSeenLocation.position.z);
        TurnToFace();
        //brain.navMeshAgent.SetDestination(brain.chasePoint.GetComponent<Transform>().position);
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }

    private void TurnToFace()
    {
        // Initially calculate rotation as a vector so that x and z components can be set to zero
        Vector3 lookRotation = Quaternion.LookRotation(lastSeenLocation.gameObject.transform.position - brain.gameObject.transform.position).eulerAngles;
        lookRotation.x = 0;
        lookRotation.z = 0;
        // All rotations should be applied as Quaternion so convert back
        lookAtRotation = Quaternion.Euler(lookRotation);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senses : MonoBehaviour
{
    private GameObject target;
    private CharacterController characterController;
    
    public float viewingAngle = 200.0f;
    public float sightRange = 200.0f;
    public float distanceToTarget;
    public float behindRange;


    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player");
        behindRange = (sightRange / 2);
    }

    public float getDistance()
    {
        return distanceToTarget;
    }

    // Checks if the target (player) is visable and returns a boolean value.
    public bool CanSeeTarget()
    {
        if (target != null) {
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (sightRange > distanceToTarget) {
                Vector3 targetDirection = target.transform.position - transform.position;
                float angle = Vector3.Angle(targetDirection, transform.forward);
                angle = System.Math.Abs(angle);
                if (angle < (viewingAngle / 2) || behindRange > distanceToTarget) {
                    CharacterController targetCharacterController = target.GetComponent<CharacterController>();
                    RaycastHit hitData;
                    LayerMask playerMask = 1 << 8;
                    LayerMask coverMask = 1 << 9;
                    LayerMask mask = coverMask | playerMask;
                    float targetHeight = targetCharacterController.height;
                    float height = characterController.height;
                    Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
                    Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y - (targetHeight / 2.0f), target.transform.position.z);
                    Vector3 direction = (targetPos - transform.position).normalized;
                    bool hit = Physics.Raycast(eyePosition, direction, out hitData, sightRange, mask.value);
                    Debug.DrawRay(eyePosition, direction * sightRange, Color.red);
                    if (hit)
                    {
                        if (hitData.collider.tag == target.gameObject.tag)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountWeapon : MonoBehaviour
{
    bool equiped;
    public GameObject mountpoint;

    // Update is called once per frame
    void Update()
    {
        transform.position = mountpoint.transform.position;
        transform.rotation = mountpoint.transform.rotation;
    }
}

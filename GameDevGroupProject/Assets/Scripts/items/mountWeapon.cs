using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountWeapon : MonoBehaviour
{
    bool equiped;
    private GameObject mountpoint;

    // Start is called before the first frame update
    void Start()
    {
        mountpoint = GameObject.FindWithTag("MountPoint");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mountpoint.transform.position;
        transform.rotation = mountpoint.transform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountWeapon : MonoBehaviour
{
    bool equiped;
    private GameObject mountpoint;
    private GameObject grip;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        mountpoint = GameObject.FindWithTag("MountPoint");
        grip = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        grip.transform.position = mountpoint.transform.position;
        grip.transform.rotation = mountpoint.transform.rotation;
    }
}

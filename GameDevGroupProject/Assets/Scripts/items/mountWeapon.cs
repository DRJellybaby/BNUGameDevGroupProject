using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountWeapon : MonoBehaviour
{
    bool equiped;
    GameObject mountpoint;
    [SerializeField]
    GameObject grip;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        mountpoint = GameObject.Find("Player/Mesh/Hands/Mountpoint");
    }

    // Update is called once per frame
    void Update()
    {
        grip.transform.position = mountpoint.transform.position;
    }
}

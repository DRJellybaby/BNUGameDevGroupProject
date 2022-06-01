using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyAttack : MonoBehaviour
{
    public float damageValue;
    private EnermyControler enermyControler;
    private PlayerControler playerControler;
    private Transform Parent;

    void Start()
    {
        playerControler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
        Parent = transform.parent;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Enermy" && transform.parent.tag != "Enermy")
            {
                enermyControler = other.GetComponent<EnermyControler>();
                enermyControler.takeDamage(damageValue);
            }
            else if (other.tag == "Player") { playerControler.takeDamage(damageValue); }

        }
    }
}

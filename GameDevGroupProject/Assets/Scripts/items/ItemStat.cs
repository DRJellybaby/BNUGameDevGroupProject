using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : MonoBehaviour
{
    public int maxInventoryCapacity;
    [SerializeField] private float damageValue = 20.0f;
    //[SerializeField] private float armourValue = 10.0f;
    //[SerializeField] private float healingValue = 10.0f;
    private EnermyControler enermyControler;
    private PlayerControler playerControler;

    void Start()
    {
        playerControler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit something");
        if (other != null)
        {
            if (other.tag == "Enermy")
            {
                enermyControler = other.GetComponent<EnermyControler>();
                Debug.Log("hit an enermy");
                enermyControler.takeDamage(damageValue);
            }
            if (other.tag == "Player") { playerControler.takeDamage(damageValue); }
            else { Debug.Log("hit nothing"); }

        }
    }
}

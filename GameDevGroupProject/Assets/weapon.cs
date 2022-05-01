using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic damage script for weapons, idea is they move with the animation, and so the colliders apply damage.
public class weapon : MonoBehaviour
{
    public int baseDamage;
    private int playerDamageMod;


    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        int damage = baseDamage * playerDamageMod;
        if (other.tag == "Enermy")
        {
            //other.TakeDamage(damage);
        }
    }
}

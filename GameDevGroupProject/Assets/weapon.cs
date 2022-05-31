using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public int baseDamage;
    private int playerDamageMod;
    private EnermyControler enermyControler;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    //basic damage script for weapons, idea is they move with the animation, and so the colliders apply damage.
    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Enermy")
            {
                enermyControler = other.GetComponent<EnermyControler>();
                Debug.Log("hit an enermy");
                enermyControler.takeDamage(playerStats.TotalDamage(this.gameObject));
            }
            if (other.tag == "Player")
            {

            }
            else { Debug.Log("hit nothing"); }

        }
    }
}

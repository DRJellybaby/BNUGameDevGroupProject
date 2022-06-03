using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weapon : MonoBehaviour
{
    private int playerDamageMod;
    private EnermyControler enermyControler;
    private PlayerControler playerControler;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    //basic damage script for weapons, idea is they move with the animation, and so the colliders apply damage.
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("tags dont work");
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
                playerControler = other.GetComponent<PlayerControler>();
                Debug.Log("Player  was hit");
                playerControler.takeDamage(playerStats.TotalDamage(this.gameObject));
            }
            else { Debug.Log("hit nothing"); }

        }
    }
}

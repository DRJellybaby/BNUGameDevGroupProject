using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnermyControler : MonoBehaviour
{
    public float enermyHealth;
    public float enermyStamina;
    public Senses sight;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enermyHealth = 100;
        sight = GetComponent<Senses>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sight.CanSeeTarget())
        {
            Debug.Log("I see you " + sight.target);
        }
    }

    public void takeDamage(float damage)
    {
        enermyHealth -= damage;
        Debug.Log("i took " + damage + " points of damage");
        if(enermyHealth <= 0)
        {
            //Destroy(this.gameObject); 
        }
    }
}

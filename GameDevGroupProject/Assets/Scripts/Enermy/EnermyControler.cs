using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnermyControler : MonoBehaviour
{
    public float enermyHealth;
    public float enermyStamina;

    [HideInInspector] public Senses sight;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;



    // Start is called before the first frame update
    public void Start()
    {
        enermyHealth = 100;
        enermyStamina = 100;
        sight = GetComponent<Senses>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
    }
}

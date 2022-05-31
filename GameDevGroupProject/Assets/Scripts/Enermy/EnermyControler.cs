using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnermyControler : MonoBehaviour
{
    public float enermyHealth;
    public float enermyStamina;

    public Senses sight;
    public CharacterController characterController;
    [HideInInspector] public Animator animator;

    public Transform origin;

    [HideInInspector] public Transform currentPosition;
    // Start is called before the first frame update
    public void Start()
    {
        enermyHealth = 100;
        enermyStamina = 100;
        sight = GetComponent<Senses>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentPosition = this.GetComponent<Transform>();
        origin.parent = null;
    }

    void update()
    {
        if (sight.CanSeeTarget()) { Debug.Log("i see player"); }
    }

    public void takeDamage(float damage)
    {
        enermyHealth -= damage;
        Debug.Log("i took " + damage + " points of damage");
        if(enermyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControler : MonoBehaviour
{
    public float bossHealth;
    public float bossStamina;

    [HideInInspector] public Senses sight;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform Player;
    [HideInInspector] public float distanceToTarget;


    [HideInInspector] public Transform currentPosition;
    // Start is called before the first frame update
    public void Start()
    {
        bossHealth = 1000;
        bossStamina = 1000;
        sight = GetComponent<Senses>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentPosition = this.GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //if (sight.CanSeeTarget()) { Debug.Log("i see player"); }
        //distanceToTarget = Vector3.Distance(sight.targetPosition, this.transform.position);
        Debug.Log(sight.distanceToTarget);
    }

    public void takeDamage(float damage)
    {
        bossHealth -= damage;
        Debug.Log("i took " + damage + " points of damage");
        if (bossHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

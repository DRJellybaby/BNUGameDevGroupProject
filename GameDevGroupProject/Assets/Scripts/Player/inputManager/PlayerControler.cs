using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 20.0f;
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private float rollDistance = 80f;
    [SerializeField] private float rollTime = 1;
    public bool isRolling;

    [SerializeField] public bool isAttacking;
    [SerializeField] public bool takenDamage;
    [SerializeField] public float attackRate;

    private bool canInteract = false; //a bool value that changes when an object finds player as a trigger (collider)

    [SerializeField] private float playerHealth;

    private Camera camera;
    public int cameraHeight;

    private Vector2 moveInput;
    private Vector3 MoveDir;
    private Vector2 mousePos;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private PlayerStats playerStats;

    private InputAction moveAction;
    private InputAction rollAction;
    private InputAction attackAction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();

        playerHealth = 100 * (1 + playerStats.vitality/100);
        Debug.Log("Player health: " + playerHealth);

        moveAction = playerInput.actions["Move"];
        rollAction = playerInput.actions["Roll"];
        attackAction = playerInput.actions["Attack"];

        camera = Camera.main;
        cameraHeight = 400;
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        MoveDir = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        mousePos = playerInput.actions["mousePosition"].ReadValue<Vector2>();
        //Debug.Log(mousePos);
        mousePos = camera.WorldToViewportPoint(mousePos);
        //Debug.Log(mousePos);
    }

    void Update()
    {
        if (!isRolling || !isAttacking) { movment(); }
        cameraFollow();
        if (rollAction.triggered)
        {
            playerAnimator.SetTrigger("Dodge");
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.green);
            StartCoroutine(dodge(MoveDir));
        }
        if (attackAction.triggered && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(attack());
        }
    }

    public void cameraFollow()
    {
        Vector3 follow = new Vector3(transform.position.x, cameraHeight, transform.position.z);
        camera.transform.position = follow;
    }

    public void movment()
    {
        if (!isAttacking)
        {
            controller.Move(MoveDir * Time.deltaTime * playerSpeed);

            playerAnimator.SetBool("Moving", true);
            if (MoveDir != Vector3.zero)
            {
                float targetAngle = moveInput.y * 90;

                if (MoveDir == Vector3.right)
                {
                    Quaternion rotation = Quaternion.Euler(0, 90, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    Quaternion rotation = Quaternion.Euler(0, targetAngle - 90, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
            }
            else { playerAnimator.SetBool("Moving", false); }
        }
    }

    IEnumerator dodge(Vector3 direction)
    {
        float startTime = Time.time;
        //Debug.Log("Start time " + startTime + " | target time: " + Time.time + rollTime);
        while (Time.time < startTime + rollTime)
        {
            isRolling = true;
            controller.Move(direction * Time.deltaTime * rollDistance);
            yield return null;
        }
        isRolling = false;
    }

    IEnumerator attack()
    {
        Debug.Log("attack");
        playerAnimator.SetTrigger("Attack");

        transform.LookAt(mousePos);

        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
    }

    public void takeDamage(float damage)
    {
        playerHealth -= damage * (1 - armourEquiped().physicalArmourValue);
        takenDamage = true;
        StartCoroutine(invulnerable(5f));
        if (playerHealth <= 0)
        {
            die();
        }
        Debug.Log("Player health: " + playerHealth);
    }

    private ItemStat armourEquiped()
    {
        foreach(Transform tr in gameObject.transform)
        {
            if(tr.tag == "Armour")
            {
                return tr.GetComponent<ItemStat>();
            }
        }
        return null;
        
    }

    protected IEnumerator invulnerable(float time)
    {
        yield return new WaitForSeconds(time);
        takenDamage = false;

    }

    public void die()
    {
        Debug.Log("player dies");
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }
}
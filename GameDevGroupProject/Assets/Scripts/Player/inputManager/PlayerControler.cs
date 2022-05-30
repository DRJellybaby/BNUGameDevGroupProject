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
    [SerializeField] public float attackRate;

    private bool canInteract = false; //a bool value that changes when an object finds player as a trigger (collider)

    [SerializeField] private float playerHealth = 100f;

    private Camera camera;
    public int cameraHeight;

    private Vector2 moveInput;
    private Vector3 MoveDir;
    private Vector3 mousePos;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private InputAction moveAction;
    private InputAction rollAction;
    private InputAction attackAction;
    private InputAction mousePosition;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        moveAction = playerInput.actions["Move"];
        rollAction = playerInput.actions["Roll"];
        attackAction = playerInput.actions["Attack"];
        mousePosition = playerInput.actions["mousePosition"];
        camera = Camera.main;
        cameraHeight = 400;
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        MoveDir = new Vector3(moveInput.x, 0, moveInput.y);

        mousePos = mousePosition.ReadValue<Vector2>();
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
        if (attackAction.triggered)
        {
            if(!isAttacking) { StartCoroutine(attack()); }
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
        isAttacking = true;

        //rotation towards mouse for attack, just need to fix issue where forward becomes up (test to see)
        Vector3 point = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camera.nearClipPlane));
        Debug.Log(point);
        transform.LookAt(point); 

        playerAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
    }

    public void takeDamage(float damage)
    {
        playerHealth = playerHealth - damage;
        if (playerHealth <= 0)
        {
            die();
        }
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
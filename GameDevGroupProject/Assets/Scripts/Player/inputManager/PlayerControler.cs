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
    private bool isRolling;
    private bool isAttacking;

    private GameObject camera;

    private Vector2 Input;
    private Vector3 MoveDir;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private InputAction moveAction;
    private InputAction rollAction;
    private InputAction attackAction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        moveAction = playerInput.actions["Move"];
        rollAction = playerInput.actions["Roll"];
        attackAction = playerInput.actions["Attack"];
        camera = GameObject.Find("Camera");
    }

    private void FixedUpdate()
    {
        Input = moveAction.ReadValue<Vector2>();
        MoveDir = new Vector3(Input.x, 0, Input.y);
    }

    void Update()
    {
        if (!isRolling || !isAttacking) {movment();}        
        cameraFollow();
        if(rollAction.triggered)
        {
            playerAnimator.SetTrigger("Dodge");
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.green);
            StartCoroutine(dodge(MoveDir));
        }
        if(attackAction.triggered)
        {
            Debug.Log("Im attacking");
            playerAnimator.SetTrigger("Attack");
        }
    }

    public void cameraFollow()
    {
        Vector3 follow = new Vector3(transform.position.x, 200, transform.position.z);
        camera.transform.position = follow;
    }

    public void movment()
    {
        //Vector3 move = new Vector3(Input.x, 0, Input.y);
        controller.Move(MoveDir * Time.deltaTime * playerSpeed);
        playerAnimator.SetBool("Moving", true);
        if (MoveDir != Vector3.zero)
        {
            float targetAngle = Input.y * 90;
            if (MoveDir == Vector3.right)
            {
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(0, targetAngle - 90, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
        }
        else { playerAnimator.SetBool("Moving", false); }
    }

    /*void rotaionToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        if (MoveDir == Vector3.right)
        {
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, mousePos - 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }*/

    IEnumerator dodge (Vector3 direction)
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
}
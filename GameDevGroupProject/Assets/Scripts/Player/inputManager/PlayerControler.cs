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
    private float rollTime;
    private bool isRolling;

    [SerializeField] private GameObject camera;

    Vector2 Input;
    Vector3 MoveDir;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private InputAction moveAction;
    private InputAction rollAction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        moveAction = playerInput.actions["Move"];
        rollAction = playerInput.actions["Roll"];
        camera = GameObject.Find("Camera");

    }

    private void FixedUpdate()
    {
        Input = moveAction.ReadValue<Vector2>();
        MoveDir = new Vector3(Input.x, 0, Input.y);
    }

    void Update()
    {
        if (!isRolling) {movment();}        
        cameraFollow();
        if(rollAction.triggered)
        {
            playerAnimator.SetTrigger("Dodge");
            rollTime = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(dodge(MoveDir));
            Debug.Log("roll " + rollTime);
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
        if (MoveDir != Vector3.zero)
        {
            playerAnimator.SetBool("Moving", true);
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
            //Debug.Log(targetAngle);
        }
        else { playerAnimator.SetBool("Moving", false); }
    }

    //public void dodge()
    //{
    //   Vector2 Input = moveAction.ReadValue<Vector2>();
    //    Vector3 move = new Vector3(Input.x, 0, Input.y);
    //    controller.Move(move * rollDistance);
    //}

    IEnumerator dodge (Vector2 direction)
    {
        /*isRolling = true;
        controller.Move(direction * Time.deltaTime * rollDistance);
        yield return null;
        isRolling = false;*/
        float startTime = Time.time;
        Debug.Log("Start time " + startTime + "target time: " + Time.time + rollTime);
        while (Time.time < startTime + rollTime)
        {
            isRolling = true;
            controller.Move(direction * Time.deltaTime * playerSpeed);

            yield return null;
        }
        isRolling = false; 
    }
}
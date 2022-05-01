using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 20.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 100;

    [SerializeField]
    private GameObject camera;

    
    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private InputAction moveAction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        moveAction = playerInput.actions["Move"];
        camera = GameObject.Find("Camera");
    }

    void Update()
    {
        movment();
        cameraFollow();
    }

    public void cameraFollow()
    {
        Vector3 follow = new Vector3(transform.position.x, 200, transform.position.z);
        camera.transform.position = follow;
    }

    public void movment()
    {
        Vector2 Input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(Input.x, 0, Input.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            playerAnimator.SetBool("Moving", true);
            float targetAngle = Input.y * 90;
            if (move == Vector3.right)
            {
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(0, targetAngle - 90, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }

            Debug.Log(targetAngle);
        }
        else { playerAnimator.SetBool("Moving", false); }

        Debug.Log(move);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    //movement variables
    public float playerSpeed;
    public float cameraHeight;
    public float rotationSpeed;

    //annimation variables
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        cameraHeight = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 10, Color.red);
        if (horizontalInputAxisCheck())
        {
            animator.SetBool("Moving", true);
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);
        }
        else {animator.SetBool("Moving", false);}

        if (verticalInputAxisCheck())
        {
            animator.SetBool("Moving", true);
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);
        }
        else { animator.SetBool("Moving", false); }

        //potentualy move to an attack section when we have an annimation for it
        Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraHeight));
        Quaternion angle = Quaternion.LookRotation(mouse, Vector3.up);
        if (Input.GetKey("space"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotationSpeed * Time.deltaTime);
        }
    }

    bool horizontalInputAxisCheck()
    {
        return (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f);
    }
    bool verticalInputAxisCheck()
    {
        return (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f);
    }
}

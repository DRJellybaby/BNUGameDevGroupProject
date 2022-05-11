using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    bool interactable = false;

    PlayerInput playerInput;

    InputAction interactAction;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable)
        {
            if( interactAction.triggered)
            {
                //run animation to open chest
                //open chest inventory
                Debug.Log("chest opening");
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //UI display "press E to open the chest" or smtg like that
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hide UI
            interactable = false;
        }
    }

    public void closedChestInventory()
    {
        //run animation to close chest
    }
}

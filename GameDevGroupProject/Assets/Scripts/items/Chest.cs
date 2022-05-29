using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    bool interactable = false;

    PlayerInput playerInput;

    public GameObject UIInteractText;


    public GameObject[] chestInventory;

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
                UIInteractText.SetActive(false);
                OpenChestInventory();
                Debug.Log("chest opening");
            }
            
        }
    }

    void OpenChestInventory()
    {
        foreach(GameObject ui in chestInventory)
        {
            ui.SetActive(true);
        }
    }

    void CloseChestInventory()
    {
        foreach (GameObject gameObject in chestInventory)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            UIInteractText.SetActive(true);
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIInteractText.SetActive(false);
            CloseChestInventory();
            interactable = false;
        }
    }

    public void ClosedChestInventory()
    {
        CloseChestInventory();
        if(UIInteractText != null)
        UIInteractText.SetActive(true);
        //run animation to close chest
    }
}

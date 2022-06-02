using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    bool interactable = false;

    public bool locked;

    PlayerInput playerInput;

    public GameObject UIInteractText;

    public GameObject UILockedText;

    Animator animator;

    public GameObject[] chestInventory;

    InputAction interactAction;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();

        animator = this.gameObject.GetComponent<Animator>();

        interactAction = playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable)
        {
            if(interactAction.triggered)
            {
                if (this.gameObject.name == "Chest")
                {
                    UIInteractText.SetActive(false);
                    OpenChestInventory();
                    Debug.Log("chest opening");
                }
                else if(gameObject.name == "Door")
                {
                    
                    if((CheckForUnlock() && locked) || !locked)
                    {
                        UIInteractText.SetActive(false);
                        animator.SetBool("Open", !animator.GetBool("Open"));
                    }
                    else
                    {
                        StartCoroutine("ShowLocked");
                    }
                }
            }
            
        }
    }

    IEnumerator Showlocked()
    {
        UILockedText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        UILockedText.SetActive(false);
    }

    bool CheckForUnlock()
    {
        if (GameObject.FindWithTag("Player").GetComponent<InventoryManagerSystem>().FindItem("Key") != null)
        {
            return true;
        }
        else return false;
    }

    void OpenChestInventory()
    {
        animator.SetBool("Open", true);
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
        animator.SetBool("Open", false);
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
    }
}

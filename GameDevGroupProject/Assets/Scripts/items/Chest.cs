/*
 * A poorly named script that actually controls all of the interactables: doors, chests, skill book
 * A little bit hard coded but it works :)
 */

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
    public GameObject[] skillBook;

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
                UIInteractText.SetActive(false);
                if (this.gameObject.name == "Chest")
                {
                    OpenChestInventory();
                    Debug.Log("chest opening");
                }
                else if(gameObject.name == "Door")
                {
                    
                    if((CheckForUnlock() && locked) || !locked)
                    {
                        this.gameObject.GetComponentInChildren<Animator>().SetBool("Door Open", true);
                    }
                    else
                    {
                        StartCoroutine("ShowLocked");
                    }
                }
                else if(this.gameObject.name == "Open Book")
                {
                    OpenSkillBook();
                }
            }
            
        }
    }

    private void OpenSkillBook()
    {
        foreach (GameObject ui in skillBook)
        {
            ui.SetActive(true);
        }
    }
 private void CloseSkillBook()
    {
        foreach (GameObject ui in skillBook)
        {
            ui.SetActive(false);
        }
    }
    IEnumerator ShowLocked()
    {
        UILockedText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        UILockedText.SetActive(false);
    }

    bool CheckForUnlock()
    {
        if (GameObject.FindWithTag("Inventory").GetComponent<InventoryManagerSystem>().FindItem("Key") != null)
        {
            Debug.Log("key exists");
            return true;
        }
        else
        {
            Debug.Log("key does not exist");
            return false;
        }
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
            if (this.gameObject.name == "Chest")
                CloseChestInventory();
            else if (gameObject.name == "Door")
                this.gameObject.GetComponentInChildren<Animator>().SetBool("Door Open", false);
            else if (gameObject.name == "Open Book")
                CloseSkillBook();
                interactable = false;
        }
    }

   

    public void ClosedChestInventory()
    {
        CloseChestInventory();
        if(UIInteractText != null)
        UIInteractText.SetActive(true);
    }
    public void ClosedSkillBook()
    {
        CloseSkillBook();
        if (UIInteractText != null)
            UIInteractText.SetActive(true);
    }

    public void ItemTaken()
    {
        chestInventory[1].SetActive(false);
    }

    public void SkillChosen(int index)
    {
        skillBook[index + 1].SetActive(false);
    }
}

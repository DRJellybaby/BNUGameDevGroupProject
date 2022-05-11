using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    // List to store GameObjects in
    private List<GameObject> inventory;

    void Awake() {
        // Create the empty list
        inventory = new List<GameObject>();
    }

    public bool Add(GameObject item) {
        // GameObjects that can be placed in the Inventory must have a pickup script component attached
        if (GetBalance(item) >= GetMax(item))
            return false;
        ItemStat itemStatScript = item.GetComponent<ItemStat>();
        if (itemStatScript) {
            inventory.Add(item);
            return true;
        }
        else {
            return false;
        }
    }

    public bool Use(GameObject searchItem)
    {
        GameObject foundItem = inventory.Find(delegate (GameObject obj) { return obj.tag == searchItem.tag; });
        if (foundItem != null)
        {
            // Remove from list
            inventory.Remove(foundItem);
            
            return true;
        }
        else return false;
    }

    int GetBalance(GameObject searchItem) {
        // Return one or more GameObjhects with a matching tag
        List<GameObject> foundItems = inventory.FindAll(delegate(GameObject obj) { return obj.tag == searchItem.tag; });
        return foundItems.Count;
    }

    // The maximum number of this type of GameObject that can be stored in the list
    int GetMax(GameObject searchItem) {
        GameObject foundItem = inventory.Find(delegate (GameObject obj) { return obj.tag == searchItem.tag; });
        if (foundItem != null)
        {
            // the pickup component property indicating the max number that can be held in the inventory
            return searchItem.GetComponent<ItemStat>().maxInventoryCapacity;
        }
        else
        {
            return 999;
        }
    }

    public List<GameObject> GetItems(string tag) {
        // Get a list of all the GameObjects with a matching type
        List<GameObject> foundItems = inventory.FindAll(delegate(GameObject obj) { return obj.tag == tag; });
        return foundItems;
    }



}

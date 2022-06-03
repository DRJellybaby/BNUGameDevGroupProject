using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerSystem : MonoBehaviour {
    // List to store GameObjects in
    private List<GameObject> inventory;

    void Awake() {
        // Create the empty list
        inventory = new List<GameObject>();
    }

    public void Add(GameObject item) {
        int max = GetMax(item);
        // GameObjects that can be placed in the Inventory must have a pickup script component attached
        ItemStat itemStatScript = item.GetComponent<ItemStat>();
        
        if (GetBalance(item) >= max || max == 999 || itemStatScript == null)
            return;
            inventory.Add(item);
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
    public GameObject FindItem(string tag)
    {
        return inventory.Find((x) => x.tag == tag);
    }


}

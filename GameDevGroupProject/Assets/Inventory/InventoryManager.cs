using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public Transform grip;

    // List to store GameObjects in
    private List<GameObject> inventory;

    void Start()
    {
        GameObject sword = GameObject.Find("medium_sword");
        inventory.Add(sword);
        if (inventory.Contains(sword))
        {
            EquipMainHandWith(sword);
        }
        
    }

    public void EquipMainHandWith(GameObject sword)
    {
        sword.transform.parent = grip.transform;
        sword.transform.localPosition = Vector3.zero;
    }
}

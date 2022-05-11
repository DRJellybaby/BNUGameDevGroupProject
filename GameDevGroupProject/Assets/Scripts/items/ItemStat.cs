using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : MonoBehaviour
{
    public int maxInventoryCapacity;
    [SerializeField] private float damageValue = 20.0f;
    private EnermyControler enermyControler;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enermy")
        {
            enermyControler = other.GetComponent<EnermyControler>();
            Debug.Log("hit an enermy");
            enermyControler.takeDamage(damageValue);
        }
        else { Debug.Log("hit nothing"); }
    }
}

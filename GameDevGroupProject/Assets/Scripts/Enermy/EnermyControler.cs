using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnermyControler : MonoBehaviour
{
    private float enermyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enermyHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        enermyHealth = -damage;
        Debug.Log("i took " + damage + " damage");
        if(enermyHealth >= 0)
        {
            Destroy(this);
        }
    }
}

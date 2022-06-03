/*
 * A simple (silly) solution to make AI work
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updatesense : Senses
{ 

    // Update is called once per frame
    void Update()
    {
        CanSeeTarget();
    }
}

//A variable holder for basic stats of weapon item, or armour
//Not all of the values have to be over 0 for the code to work
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : MonoBehaviour
{
    public int maxInventoryCapacity;
    public float damageValue = 0, magicDamageValue = 0, attackSpeedValue = 0, staminaCostValue = 0, physicalArmourValue = 0,
        magicArmourValue = 0, weightValue = 0, healingValue = 0;
}

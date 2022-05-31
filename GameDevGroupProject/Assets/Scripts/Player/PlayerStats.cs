using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public float strength, dexterity, intelligence, stamina, vitality;
    ItemStat itemStat;

    private void Awake()
    {
        
    }

    //depends on your armour/magic resist thi will be a check trough inventory equiped armour
    public float TotalDamage(GameObject weapon)
    {
        itemStat = weapon.GetComponent<ItemStat>();
        float temp = 0;
            if(weapon.tag == "Melee")
            temp = itemStat.damageValue * (1 + strength/100);
            if(weapon.tag == "Ranged")
            temp = itemStat.damageValue * (1 + dexterity / 100);

        return temp;
    }
    public void AddStatstrength(float howMuch)
    {
        strength += howMuch;
    }
    public void AddStatDexterity(float howMuch)
    {
        dexterity += howMuch;
    }
    public void AddStatIntelligence(float howMuch)
    {
        intelligence += howMuch;
    }
    public void AddStatStamina(float howMuch)
    {
        stamina += howMuch;
    }
    public void AddStatVitality(float howMuch)
    {
        vitality += howMuch;
    }
    public void RemoveStatstrength(float howMuch)
    {
        strength -= howMuch;
    }
    public void RemoveStatDexterity(float howMuch)
    {
        dexterity -= howMuch;
    }
    public void RemoveStatIntelligence(float howMuch)
    {
        intelligence -= howMuch;
    }
    public void RemoveStatStamina(float howMuch)
    {
        stamina -= howMuch;
    }
    public void RemoveStatVitality(float howMuch)
    {
        vitality -= howMuch;
    }


}

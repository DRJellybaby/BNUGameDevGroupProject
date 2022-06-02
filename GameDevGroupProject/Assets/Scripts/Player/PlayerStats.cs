using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public float strength, dexterity, intelligence, stamina, staminaRefillRate, maxStamina, vitality;
    ItemStat itemStat;

    bool replenish = true;

    private void Awake()
    {
        
    }

    private void Start()
    {
        stamina = maxStamina;
        StartCoroutine("RefillStamina");
    }

    IEnumerator RefillStamina ()
    {
        while (true)
        {
            if (stamina < maxStamina && replenish)
                stamina += staminaRefillRate * 0.1f;
            if (stamina > maxStamina)
                stamina = maxStamina;
            yield return new WaitForSeconds(0.4f);
            Debug.Log(stamina);
        }
    }

    public void UseStamina(float staminaToUse)
    {
        if(stamina > 0)
        {
            if(stamina - staminaToUse > 0)
            {
                stamina -= staminaToUse;
            }
            else
            {
                StartCoroutine("PlayerOverheat");
                stamina = 0;
            }
        }
    }

    IEnumerator PlayerOverheat()
    {
        gameObject.GetComponent<PlayerControler>().SetCanInteract(false);
        replenish = false;
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<PlayerControler>().SetCanInteract(true);
        replenish = true;
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
    public void AddStatStrength(float howMuch)
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
    public void RemoveStatStrength(float howMuch)
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

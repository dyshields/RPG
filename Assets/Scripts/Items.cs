using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP;
    public bool affectMP;
    public bool affectStr;
    public bool affectDef;
    [Header("Weapon / Armor Details")]
    public int weaponStrength;
    public int armorStrength;



    void Start()
    {
        
    } 

    
    void Update()
    {
        
    }

    public void Use (int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if (isItem)
        {
            if(affectHP)
            {
                selectedChar.currentHP += amountToChange;
                if(selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }

                AudioManager.instance.PlaySFX(7);
            }
       
            if (affectMP)
            {
                selectedChar.currentMP += amountToChange;
                if (selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }

                AudioManager.instance.PlaySFX(6);
            }

            if (affectStr)
            {
                selectedChar.strength += amountToChange;
            }
        }

        if (isWeapon)
        {
            if(selectedChar.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedWeapon);
            }

            selectedChar.equippedWeapon = itemName;
            selectedChar.weaponPower = weaponStrength;

            AudioManager.instance.PlaySFX(1);
        }

        if (isArmor)
        {
            if (selectedChar.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedArmor);
            }

            selectedChar.equippedArmor = itemName;
            selectedChar.armorPower = armorStrength;

            AudioManager.instance.PlaySFX(1);
        }

        GameManager.instance.RemoveItem(itemName);
    }
}

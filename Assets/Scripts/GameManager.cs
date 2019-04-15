using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Items[] referenceItems;
    public int currentGold;
    public bool shopActive;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("BLA");
            RemoveItem("Health Potion");
            RemoveItem("Bleep");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    public Items GetItemDetails (string itemToGrab)
    {
        for(int i=0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }



        // Unity doesn't necessarily know that we won't get to this point; return null is to prevent infinite looping
        return null;
    }

    // Sort function that looks at each of the slots in the Items window
    // Is this slot blank? Then pull down the next closest item into this spot, then delete the old one
    // Make sure that code doesn't search outside the array > "itemsHeld.Length -1"
    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            // Needs to be set to false so it doesn't just loop once
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] !="")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;
        for (int i =0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] ==itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if(foundSpace)
        {
            bool itemExists = false;
            for (int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }
            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does Not Exist!!");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem (string itemToRemove)
    {
        bool founditem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                founditem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }

        if (founditem)
        {
            numberOfItems[itemPosition]--;

            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
        } else
        {
            Debug.LogError("Couldn't find " + itemToRemove);
        }
    }

    public void SaveData()
    {

        // Saves character position
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        // Saves character stats
        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1);
            } else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentExp", playerStats[i].currentEXP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxMP", playerStats[i].maxMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Defense", playerStats[i].defense);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_WpnPwr", playerStats[i].weaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_ArmrPwr", playerStats[i].armorPower);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_EquippedWpn", playerStats[i].equippedWeapon);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_EquippedArmr", playerStats[i].equippedArmor);
        }
        // Saves inventory data
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }
    }

    public void LoadData()
    {
        // Loads player position in game world
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));


        for(int i=0; i < playerStats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_level");
            playerStats[i].currentEXP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentHP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentMP");
            playerStats[i].maxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxMP");
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Strength");
            playerStats[i].defense = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Defense");
            playerStats[i].weaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WpnPwr");
            playerStats[i].armorPower = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmrPwr");
            playerStats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquippedWpn");
            playerStats[i].equippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_EquippedArmr");
        }

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }
}

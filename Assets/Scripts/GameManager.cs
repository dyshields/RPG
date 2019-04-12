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
        }

    }

    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
    }
}

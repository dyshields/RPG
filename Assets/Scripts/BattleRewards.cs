using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewards : MonoBehaviour
{
    public static BattleRewards instance;
    public Text xpText;
    public Text itemText;
    public GameObject rewardScreen;
    public string[] rewardItems;
    public int xpEarned;


    void Start()
    {
        instance = this;
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(54, new string[] { "Iron Sword", "Iron Armor"} );
        }
    }

    public void OpenRewardScreen(int xp, string[] rewards)
    {
        xpEarned = xp;
        rewardItems = rewards;

        xpText.text = "Everyone earns " + xpEarned + "xp!";
        itemText.text = "";

        for(int i=0; i < rewardItems.Length; i++)
        {
            // \n is the enter key; will return a new line
            itemText.text += rewards[i] + "\n";
        }

        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        for(int i =0; i < GameManager.instance.playerStats.Length; i++)
        {
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].AddEXP(xpEarned);
            }

        }

        for(int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }


        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;
    }
}

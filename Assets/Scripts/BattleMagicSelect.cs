using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleMagicSelect : MonoBehaviour
{

    public string spellName;
    public int spellCost;
    public Text nameText;
    public Text costText;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Press()
    {

        // Checks to see if player has enough MP to cast spell
        if (BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP >= spellCost)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP -= spellCost;
        }
        else
        {
            // Add message for insufficient MP
            BattleManager.instance.battleNotice.theText.text = "Insufficient MP :(";
            BattleManager.instance.battleNotice.Activate();
            BattleManager.instance.magicMenu.SetActive(false);
        }
        
        
    }
}

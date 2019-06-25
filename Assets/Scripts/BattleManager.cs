﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{


    public static BattleManager instance;
    private bool battleActive;
    public GameObject battleScene;
    public Transform[] enemyPositions;
    public Transform[] playerPositions;
    public BattleChars[] playerPrefabs;
    public BattleChars[] enemyPrefabs;
    public List<BattleChars> activeBattlers = new List<BattleChars>();

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            BattleStart(new string[] {"Eyeball", "Skeleton", "Spider", "Wizard" });
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if(!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;

            // Battle Scene needs to instantiate at the camera position; don't change z value, though
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);
            AudioManager.instance.PlayMusic(0);

            for (int i=0; i < playerPositions.Length; i++)
            {
                if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for(int j =0; j < playerPrefabs.Length; j++)
                    {
                        if(playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChars newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            newPlayer.transform.parent = playerPositions[i];
                            activeBattlers.Add(newPlayer);

                            CharStats thePlayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHP = thePlayer.currentHP;
                            activeBattlers[i].maxHP = thePlayer.maxHP;
                            activeBattlers[i].currentMP = thePlayer.currentMP;
                            activeBattlers[i].maxMP = thePlayer.maxMP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defense = thePlayer.defense;
                            activeBattlers[i].wpnPower = thePlayer.weaponPower;
                            activeBattlers[i].armorPower = thePlayer.armorPower;
                        }
                    }
                }
            }

            for (int i=0; i < enemiesToSpawn.Length; i++)
            {
                if(enemiesToSpawn[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                            BattleChars newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            newEnemy.transform.parent = enemyPositions[i];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChars : MonoBehaviour
{
    public bool isPlayer;
    public string charName;
    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;
    public int strength;
    public int defense;
    public int wpnPower;
    public int armorPower;
    public bool hasDied;
    public string[] movesAvailable;
    public SpriteRenderer theSprite;
    public Sprite deadSprite;
    public Sprite aliveSprite;
    private bool shouldFade;
    public float fadeSpeed = 1f;


    void Start()
    {
        
    }


    void Update()
    {
        // Adds a fade effect for dead enemies through the Sprite Renderer
        // Colors in unity have values of 1-255 for RGBA, but only 0-1 in script
        if(shouldFade)
        {
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadeSpeed * Time.deltaTime),
                Mathf.MoveTowards(theSprite.color.g, 0f, fadeSpeed * Time.deltaTime),
                Mathf.MoveTowards(theSprite.color.b, 0f, fadeSpeed * Time.deltaTime),
                Mathf.MoveTowards(theSprite.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(theSprite.color.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void EnemyFade()
    {
        shouldFade = true;
    }
}

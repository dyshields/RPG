using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour

{
    public Text damageText;
    public float lifetime = 1f;
    public float movespeed = 1f;
    public float placementJitter = .5f;


    
    void Start()
    {
        
    }

    
    void Update()
    {
        Destroy(gameObject, lifetime);
        transform.position += new Vector3(0f, movespeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    // Purpose of this script is to create a player object in a scene if there isn't one already
    public GameObject player;

    void Start()
    {
        if(PlayerController.instance == null)
        {
            Instantiate(player);
        }
    }

    
    void Update()
    {
        
    }
}

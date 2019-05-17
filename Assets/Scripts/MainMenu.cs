﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;
    public GameObject continueButton;
    public string loadGameScene;


    // Code determines if PlayerPrefs data exists or not, and enabled Continue button if so
    void Start()
    {
        if(PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

 
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    // Script is referencing black image; toggles the Alpha value for a fade effect
    public Image fadeScreen;
    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;
    public float fadeSpeed = 1f;
    public static UIFade instance;

    void Start() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)
            );

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }

        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)
            );

            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack() 
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}

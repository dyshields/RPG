using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] music;
    public AudioSource[] sfx;
    public static AudioManager instance;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            PlayMusic(3);
        }
    }

    public void PlaySFX(int soundToPlay)
    {

        if (soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }
    }

    public void PlayMusic (int musicToPlay)
    {
        if (!music[musicToPlay].isPlaying)
        {
            StopMusic();

            if (musicToPlay < music.Length)
            {
                music[musicToPlay].Play();
            }
        }
    }

    public void StopMusic()
    {
        for(int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }
    }
}

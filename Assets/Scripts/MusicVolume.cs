using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource musicSource = GetComponent<AudioSource>();

        int currentVolume = PlayerPrefs.GetInt("soundVolume", 5);

        currentVolume = currentVolume > 5 ? 5 : currentVolume;

        musicSource.volume = currentVolume * 0.2f;

        //Debug.Log("Volume set to " + (currentVolume * 0.2f * 100) + "% of the original value");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider musicVol;
    public AudioSource audioSource;

    void Update()
    {
        audioSource.volume = musicVol.value;
    }
}

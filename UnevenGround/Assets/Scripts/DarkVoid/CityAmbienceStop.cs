using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCityAudio : MonoBehaviour
{
    public AudioSource cityAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cityAudio.isPlaying)
            {
                cityAudio.Stop();
            }
            else
            {
                cityAudio.Play();
            }
        }
    }
}
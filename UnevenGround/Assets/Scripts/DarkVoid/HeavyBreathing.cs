using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBreathing : MonoBehaviour
{
    public AudioSource breathingAudio;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            breathingAudio.Play();
        }
    }
}

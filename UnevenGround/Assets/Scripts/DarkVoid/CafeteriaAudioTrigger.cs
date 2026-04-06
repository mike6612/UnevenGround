using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeteriaAudioTrigger : MonoBehaviour
{
    public AudioSource cafeteriaAudio;
    private bool canTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
        {
            canTrigger = false;
            if (cafeteriaAudio.isPlaying)
                cafeteriaAudio.Stop();
            else
                cafeteriaAudio.Play();

        }
    }

}

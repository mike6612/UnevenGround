using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAudio : MonoBehaviour
{
    public AudioSource windAudio;
    private bool canTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
        {
            canTrigger = false;

            if (windAudio.isPlaying)
                windAudio.Stop();
            else
                windAudio.Play();

            Invoke(nameof(ResetTrigger), 0.5f);
        }
    }

    void ResetTrigger()
    {
        canTrigger = true;
    }
}

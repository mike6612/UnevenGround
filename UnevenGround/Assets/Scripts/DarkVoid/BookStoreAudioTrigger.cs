using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreAudioTrigger : MonoBehaviour
{
    public AudioSource bookStoreAudio;
    public bool canTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
        {
            canTrigger = false;
            if (bookStoreAudio.isPlaying)
                bookStoreAudio.Stop();
            else
                bookStoreAudio.Play();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreAudioTrigger : MonoBehaviour
{
    public AudioSource bookStoreAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bookStoreAudio != null)
            {
                bookStoreAudio.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAwayWordsDarkVoid : MonoBehaviour
{
    public GameObject word;
    public AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            Osiciliate osc = word.GetComponent<Osiciliate>();
            if (osc != null)
            {
                osc.TriggerExit();
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }

        }
    }
}
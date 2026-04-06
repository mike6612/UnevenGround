using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DizzinessScript : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject typographyObject;

    public AudioClip dizziness;
    public bool canTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
            canTrigger = false;
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {

        audioSource = GetComponent<AudioSource>();

        typographyObject.SetActive(true);

        audioSource.loop = false;
        audioSource.clip = dizziness;

        audioSource.Play();
        yield return null;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StomachGrowl : MonoBehaviour
{
    public AudioClip stomachGrowl;


    private AudioSource audioSource;
    public CAVE2WandNavigator navigator;

    public GameObject typography;
    public GameObject trigger;
    private bool played = false;
    float originalSpeed;
    float originalRotation;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played)
        {
            played = true;

            originalSpeed = navigator.movementScale;
            originalRotation = navigator.turnSpeed;

            navigator.movementScale = 0;
            navigator.turnSpeed = 0;

            audioSource.PlayOneShot(stomachGrowl);

            StartCoroutine(showTypography()); 

         
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator showTypography()
    {
        typography.SetActive(true);

        yield return new WaitForSeconds(5f);

        typography.SetActive(false);

        navigator.turnSpeed = originalRotation;
        navigator.movementScale = originalSpeed;

        Destroy(gameObject);
    }
}
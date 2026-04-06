using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreIntro : MonoBehaviour
{
    private bool played = false;

    float originalSpeed;
    float originalRotation;
    public CAVE2WandNavigator navigator;

    //private AudioSource audioSource;
    //public AudioClip frustrated;

    public GameObject typography;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !played)
        {
            played = true;

            originalSpeed = navigator.movementScale;
            originalRotation = navigator.turnSpeed;

            navigator.movementScale = 0;
            navigator.turnSpeed = 0;

            //audioSource.clip = frustrated;
            //audioSource.Play();



            StartCoroutine(showTypography());

        }
    }



    IEnumerator showTypography()
    {
        typography.SetActive(true);

        yield return new WaitForSeconds(4f);

        typography.SetActive(false);

        navigator.turnSpeed = originalRotation;
        navigator.movementScale = originalSpeed;

    
    }

}

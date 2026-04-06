using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DizzinessScript : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject typographyObject;

    public bool canTrigger = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
            canTrigger = false;
            StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {

  
        typographyObject.SetActive(true);

      
        audioSource.Play(); 
        yield return null;

    }


}

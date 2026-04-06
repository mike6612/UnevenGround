
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDescriptionTrigger : MonoBehaviour
{
    public GameObject projectDescription;    
    
    public GameObject title;
    public CAVE2WandNavigator navigator;
    public GameObject trigger;
    public AudioSource windAudio;


    void OnTriggerEnter(Collider other)
    {
        projectDescription.SetActive(false);

        StartCoroutine(Title());
    }


    IEnumerator Title()
    {
        float originalTurnSpeed = navigator.turnSpeed;
        float originalMovementScale = navigator.movementScale; 
        //navigator.DisableMovement();
        navigator.turnSpeed = 0;
        navigator.movementScale = 0;
        title.SetActive(true);
        yield return new WaitForSeconds(3f);


        trigger.SetActive(false);
        title.SetActive(false);
        windAudio.Play();

        //navigator.EnableMovement();
        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;


    }


}
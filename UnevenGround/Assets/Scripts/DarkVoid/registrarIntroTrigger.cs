using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class registrarIntroTrigger : MonoBehaviour
{
    public GameObject typographyObject;   // Assign in Inspector
    public float displayTime = 5f;
    public CAVE2WandNavigator navigator;
    public CAVE2InputManager inputManager;
    public bool disableTurning;
    public AudioClip maleSigh;
    
    public GameObject trigger;

    private AudioSource audioSource;


    float originalTurnSpeed;
    float originalMovementScale;


    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if(!hasTriggered && other.CompareTag("Player"))
        { 

            hasTriggered = true;

            originalMovementScale = navigator.movementScale;
            originalTurnSpeed = navigator.turnSpeed;

            navigator.movementScale = 0;
            navigator.turnSpeed = 0;

          

            StartCoroutine(ShowTypography());

        }
    }

IEnumerator ShowTypography()
    {
        audioSource = GetComponent<AudioSource>();

        typographyObject.SetActive(true);
        audioSource.loop = false; 

        audioSource.clip = maleSigh;

        audioSource.Play();
        yield return new WaitForSeconds(4f);



        trigger.SetActive(false);

        navigator.EnableMovement();

        typographyObject.SetActive(false);

        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;
    }



}
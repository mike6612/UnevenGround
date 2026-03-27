using UnityEngine;

using System.Collections;

public class startDarkVoidTrigger : MonoBehaviour
{

    public GameObject title;
    public CAVE2WandNavigator navigator;
    public GameObject trigger;

    void OnTriggerEnter(Collider other)
    {

        StartCoroutine(Title());
    }


    IEnumerator Title()
    {
        float originalTurnSpeed = navigator.turnSpeed;
        float originalMovementScale = navigator.movementScale; 
        //navigator.DisableMovement();
        navigator.turnSpeed = 0;
        navigator.movementScale = 0;
        yield return new WaitForSeconds(3f);


        trigger.SetActive(false);
        title.SetActive(false);
        //navigator.EnableMovement();
        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;


    }
}


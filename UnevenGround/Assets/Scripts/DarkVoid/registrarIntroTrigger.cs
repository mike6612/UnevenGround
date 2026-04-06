using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using static UnityEngine.GraphicsBuffer;

public class registrarIntroTrigger : MonoBehaviour
{
    public GameObject typographyObject;   // Assign in Inspector
    public float displayTime = 5f;
    public CAVE2WandNavigator navigator;
    public GameObject player;
    public CAVE2InputManager inputManager;
    public bool disableTurning;
    public AudioClip maleSigh;
    
    public GameObject trigger;

    private AudioSource audioSource;


    float originalTurnSpeed;
    float originalMovementScale;


    private bool hasTriggered = false;
    public float moveSpeed = 0.002f;

    public Transform target;

    public Transform cameraControler;
    public Transform cameraControllerTarget;


    void OnTriggerEnter(Collider other)
    {
        if(!hasTriggered && other.CompareTag("Player"))
        { 

            hasTriggered = true;


            originalMovementScale = navigator.movementScale;
            originalTurnSpeed = navigator.turnSpeed;


            navigator.movementScale = 0;
            navigator.turnSpeed = 0;
            StartCoroutine(Sequence());

        }
    }


IEnumerator Sequence()
    {
        yield return StartCoroutine(MovePerson(target.position, cameraControllerTarget.rotation));
        yield return StartCoroutine(ShowTypography());
    }
IEnumerator ShowTypography()
    {
        audioSource = GetComponent<AudioSource>();

        typographyObject.SetActive(true);
        audioSource.loop = false; 

        audioSource.clip = maleSigh;

        audioSource.Play();
        yield return new WaitForSeconds(5f);



        trigger.SetActive(false);

        navigator.EnableMovement();

        typographyObject.SetActive(false);

        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;
    }


    IEnumerator MovePerson(UnityEngine.Vector3 targetPos, UnityEngine.Quaternion targetRot)
    {
        while (UnityEngine.Vector3.Distance(player.transform.position, targetPos) > 0.01f ||
               Mathf.Abs(Mathf.DeltaAngle(player.transform.eulerAngles.y, targetRot.eulerAngles.y)) > 0.1f)
        {
            player.transform.position = UnityEngine.Vector3.MoveTowards(player.transform.position, targetPos, moveSpeed * Time.deltaTime);
            cameraControler.transform.rotation = UnityEngine.Quaternion.RotateTowards(cameraControler.transform.rotation, targetRot, moveSpeed * 100f * Time.deltaTime);

            yield return null;
        }

        player.transform.position = targetPos;
        cameraControler.transform.rotation = targetRot;
    }

}



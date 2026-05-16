using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreOutro : MonoBehaviour
{


    public CAVE2WandNavigator navigator;
    //public GameObject player;
    //public Transform playerTarget;

    //public Transform cameraControler;
    //public Transform cameraControllerTarget;
    public float moveSpeed = 2f;


    public GameObject bookStoreEndingSentence;


    public bool canTrigger = true;

    public GameObject exitPortal;
    //private float originalTurnSpeed;
    //private float originalMovementScale;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
            canTrigger = false;
            StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        //originalTurnSpeed = navigator.turnSpeed;
        //originalMovementScale = navigator.movementScale;
        //navigator.turnSpeed = 0f;
        //navigator.movementScale = 0f;

        //yield return StartCoroutine(MovePerson(playerTarget.position, cameraControllerTarget.rotation));

        yield return StartCoroutine(ShowTypography());

    }



    //IEnumerator MovePerson(UnityEngine.Vector3 targetPos, UnityEngine.Quaternion targetRot)
    //{
    //    while (UnityEngine.Vector3.Distance(player.transform.position, targetPos) > 0.01f ||
    //           Mathf.Abs(Mathf.DeltaAngle(cameraControler.transform.eulerAngles.y, targetRot.eulerAngles.y)) > 0.1f)
    //    {
    //        player.transform.position = UnityEngine.Vector3.MoveTowards(player.transform.position, targetPos, moveSpeed * Time.deltaTime);
    //        cameraControler.transform.rotation = UnityEngine.Quaternion.RotateTowards(cameraControler.transform.rotation, targetRot, moveSpeed * 100f * Time.deltaTime);

    //        yield return null;
    //    }

    //    player.transform.position = targetPos;
    //    cameraControler.transform.rotation = targetRot;
    //}

    IEnumerator ShowTypography()
    {
        yield return new WaitForSeconds(2f);

        bookStoreEndingSentence.SetActive(true);

        yield return new WaitForSeconds(4f);
        //navigator.turnSpeed = originalTurnSpeed;
        //navigator.movementScale = originalMovementScale;
        exitPortal.SetActive(true);

    }

}
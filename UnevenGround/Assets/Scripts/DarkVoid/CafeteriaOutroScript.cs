using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CafeteriaOutroScript : MonoBehaviour
{

    public GameObject foodOutroTypography;
    public GameObject exitPortal;


    public CAVE2WandNavigator navigator;
    //public GameObject player;
    //public Transform playerTarget;

    //public Transform cameraControler;
    //public Transform cameraControllerTarget;
    public float moveSpeed = 2f;

    public bool canTrigger = true;


    public AudioClip maleRelief;


    private AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
            canTrigger = false;
            StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
    

        //float originalTurnSpeed = navigator.turnSpeed;
        //float originalMovementScale = navigator.movementScale;
        //navigator.turnSpeed = 0f;
        //navigator.movementScale = 0f;

        //yield return StartCoroutine(MovePerson(playerTarget.position, cameraControllerTarget.rotation));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(ShowTypography());
        //navigator.turnSpeed = originalTurnSpeed;
        //navigator.movementScale = originalMovementScale;

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

        audioSource = GetComponent<AudioSource>();
        foodOutroTypography.SetActive(true);


        audioSource.loop = false;

        audioSource.clip = maleRelief;

        audioSource.Play();


        yield return new WaitForSeconds(4f);
        exitPortal.SetActive(true);
    }

}




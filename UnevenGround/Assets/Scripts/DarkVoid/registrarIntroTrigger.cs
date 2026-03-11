using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class registrarIntroTrigger : MonoBehaviour
{
    public GameObject typographyObject;   // Assign in Inspector
    public Transform playerHead;          // Assign XR Camera
    public float displayTime = 5f;
    public CAVE2WandNavigator navigator;
    public CAVE2InputManager inputManager;
    public bool disableTurning;

    void DisableHeadRotationKeys()
    {
        inputManager.simulatorHeadRotateL = KeyCode.None;
        inputManager.simulatorHeadRotateR = KeyCode.None;
    }

    void EnableHeadRotationKeys()
    {
        inputManager.simulatorHeadRotateL = KeyCode.Q;
        inputManager.simulatorHeadRotateR = KeyCode.E;
    }


    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if(!hasTriggered && other.CompareTag("Player"))
        { 
            StartCoroutine(ShowTypography());
        }
    }

IEnumerator ShowTypography()
    {
        hasTriggered = true;
        navigator.DisableMovement();
        DisableHeadRotationKeys();


        typographyObject.SetActive(true);

        // Parent FIRST
        typographyObject.transform.SetParent(playerHead, true);

        // Reset transform RELATIVE to camera
        typographyObject.transform.localPosition = new Vector3(0f, -3.79f, 8.07f);


        typographyObject.transform.LookAt(playerHead);

        Vector3 euler = typographyObject.transform.eulerAngles;
        euler.x = 0f; // lock X
        typographyObject.transform.eulerAngles = euler;


        Vector3 worldPos = typographyObject.transform.position;
        worldPos.x += 3f;
        typographyObject.transform.position = worldPos;


        // Now scale it
        typographyObject.transform.localScale = Vector3.one * 2f;



        // Move left over time
        float timer = 0f;
        float speed = 0.5f;

        while (timer < 13)
        {
            Vector3 pos = typographyObject.transform.position;
            pos.x -= speed * Time.deltaTime; // move left
            typographyObject.transform.position = pos;

            timer += Time.deltaTime;
            yield return null;
        }


        EnableHeadRotationKeys();
        navigator.EnableMovement();


        // Unparent and hide
        typographyObject.transform.SetParent(null);
        typographyObject.SetActive(false);
    }



}
using UnityEngine;
using System.Collections;

public class registrarIntroTrigger : MonoBehaviour
{
    public GameObject typographyObject;   // Assign in Inspector
    public Transform playerHead;          // Assign XR Camera
    public float displayTime = 5f;
    public GameObject petitionQuad;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if(!hasTriggered && other.CompareTag("Player"))
        {
            petitionQuad.SetActive(true);
            StartCoroutine(ShowTypography());
        }
    }

IEnumerator ShowTypography()
    {
        hasTriggered = true; 

        typographyObject.SetActive(true);

        // Parent FIRST
        typographyObject.transform.SetParent(playerHead);

        // Reset transform RELATIVE to camera
        typographyObject.transform.localPosition = new Vector3(0f, -3.79f, 14f);


        typographyObject.transform.LookAt(playerHead);

        Vector3 euler = typographyObject.transform.eulerAngles;
        euler.x = 0f; // lock X
        typographyObject.transform.eulerAngles = euler;



        // Now scale it
        typographyObject.transform.localScale = Vector3.one * 2f;

        yield return new WaitForSeconds(displayTime);

        // Unparent and hide
        typographyObject.transform.SetParent(null);
        typographyObject.SetActive(false);
    }
}
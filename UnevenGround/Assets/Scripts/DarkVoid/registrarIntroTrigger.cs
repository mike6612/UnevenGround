using UnityEngine;
using System.Collections;

public class registrarIntroTrigger : MonoBehaviour
{
    public GameObject typographyObject;   // Assign in Inspector
    public Transform playerHead;          // Assign XR Camera
    public float displayTime = 30f;

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

        typographyObject.SetActive(true);

        // Parent FIRST
        typographyObject.transform.SetParent(playerHead);

        // Reset transform RELATIVE to camera
        typographyObject.transform.localPosition = new Vector3(1.64f, -3.15f, 11.5f);
        

        // Now scale it
        typographyObject.transform.localScale = Vector3.one * 2f;

        yield return new WaitForSeconds(displayTime);

        // Unparent and hide
        typographyObject.transform.SetParent(null);
        typographyObject.SetActive(false);
    }
}
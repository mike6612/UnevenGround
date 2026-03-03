using TMPro;
using UnityEngine;
using System.Collections;

public class registrarFeeSheetTrigger : MonoBehaviour
{
    public GameObject quad;   
    public GameObject petitionQuad;          // assign your quad
    public Transform playerHead;         // assign VR camera (Main Camera)
    public float faceDistance = 1.2f;    // how far from face
    public float moveSpeed = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isRunning = false;
    public TextMeshProUGUI feeText;
    public TextMeshProUGUI feeTitleText;
    public CAVE2WandNavigator navigator;
    
    private bool hasPlayed = false;


    void Start()
    {
        originalPosition = quad.transform.position;
        originalRotation = quad.transform.rotation;
        quad.SetActive(false);
        petitionQuad.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(ShowSequence());
        }
    }

    IEnumerator ShowSequence()
    {
        isRunning = true;
        navigator.DisableMovement();

        quad.SetActive(true);

        // Step 1: Wait at desk
        yield return new WaitForSeconds(2f);

        // Step 2: Move to face
        Vector3 facePosition = playerHead.position + playerHead.forward * faceDistance;
        Quaternion faceRotation = Quaternion.LookRotation(playerHead.forward);

        yield return StartCoroutine(MoveQuad(facePosition, faceRotation));

        // Step 3: Show hidden fees 
        string[] fees = {
            "Processing Fee:                $150.00",
            "Convenience Fee:               $80.00",
            "Administrative Fee:             $55.00",
        };
        string text  = "Item                                   Total";
 
        feeTitleText.text +=  text;
        yield return new WaitForSeconds(1.5f);

        foreach (string fee in fees)
        {
            feeText.text += fee + "\n";
            feeText.text += "_____________________________________\n\n";
            
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(2f);

        // Step 4: Move back to desk
        yield return StartCoroutine(MoveQuad(originalPosition, originalRotation));
        quad.SetActive(false);

        hasPlayed = true;
        navigator.EnableMovement();
        petitionQuad.SetActive(true);
    }



    IEnumerator MoveQuad(Vector3 targetPos, Quaternion targetRot)
    {
        while (Vector3.Distance(quad.transform.position, targetPos) > 0.01f)
        {
            quad.transform.position = Vector3.Lerp(
                quad.transform.position,
                targetPos,
                Time.deltaTime * moveSpeed
            );

            quad.transform.rotation = Quaternion.Lerp(
                quad.transform.rotation,
                targetRot,
                Time.deltaTime * moveSpeed
            );

            yield return null;
        }

        quad.transform.position = targetPos;
        quad.transform.rotation = targetRot;
    }
}
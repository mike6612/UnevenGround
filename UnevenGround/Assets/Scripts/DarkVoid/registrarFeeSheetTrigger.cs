


using UnityEngine;
using System.Collections;

public class registrarFeeSheetTrigger : MonoBehaviour
{
    public GameObject quad;              // assign your quad
    public Transform playerHead;         // assign VR camera (Main Camera)
    public float faceDistance = 1.2f;    // how far from face
    public float moveSpeed = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isRunning = false;

    void Start()
    {
        originalPosition = quad.transform.position;
        originalRotation = quad.transform.rotation;
        quad.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRunning)
        {
            StartCoroutine(ShowSequence());
        }
    }

    IEnumerator ShowSequence()
    {
        isRunning = true;

        quad.SetActive(true);

        // Step 1: Wait at desk
        yield return new WaitForSeconds(2f);

        // Step 2: Move to face
        Vector3 facePosition = playerHead.position + playerHead.forward * faceDistance;
        Quaternion faceRotation = Quaternion.LookRotation(playerHead.forward);

        yield return StartCoroutine(MoveQuad(facePosition, faceRotation));

        // Step 3: Show hidden fees (placeholder delay)
        Debug.Log("Hidden Fee 1");
        yield return new WaitForSeconds(2f);
        Debug.Log("Hidden Fee 2");
        yield return new WaitForSeconds(2f);
        Debug.Log("Hidden Fee 3");

        yield return new WaitForSeconds(2f);

        // Step 4: Move back to desk
        yield return StartCoroutine(MoveQuad(originalPosition, originalRotation));

        isRunning = false;
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
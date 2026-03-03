using UnityEngine;
using System.Collections;

public class PaperClickable : CAVE2Interactable
{
    public bool hasPlayed = false;
    public GameObject quad;  
    
    public Transform playerHead;  
    public float faceDistance = 1.2f;    // how far from face
    public float moveSpeed = 2f; 
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public CAVE2WandNavigator navigator;


    void Update()
    {
        UpdateWandOverTimer();
    }
    void Start()
    {
        originalPosition = quad.transform.position;
        originalRotation = quad.transform.rotation;
        quad.SetActive(false);
    }



    new void OnWandButtonDown(CAVE2.WandEvent evt)
    {
        if(evt.button == CAVE2.Button.Button1 && wandPointing && !hasPlayed)
        {
            StartCoroutine(ShowSequence());
        }
    }

    IEnumerator ShowSequence()
    {
        navigator.DisableMovement();
        Vector3 facePosition = playerHead.position + playerHead.forward * faceDistance;
        Quaternion faceRotation = Quaternion.LookRotation(playerHead.forward);

        // Move paper in front of the player
        yield return StartCoroutine(MoveQuad(facePosition, faceRotation));

        // Show signature (could be TextMeshPro, texture, or sprite)
        yield return new WaitForSeconds(5f);

        // Move back and hide
        yield return StartCoroutine(MoveQuad(originalPosition, originalRotation));
        quad.SetActive(false);

        navigator.EnableMovement();
        hasPlayed = true;
    }

     IEnumerator MoveQuad(Vector3 targetPos, Quaternion targetRot){
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
using UnityEngine;
using UnityEngine.UI; // old
using System.Collections;
using UnityEngine.Rendering;

public class PaperClick : CAVE2Interactable
{
    public bool hasTriggered = false;
    public GameObject quad;  
    
    public Transform playerHead;  
    public float faceDistance = 1.2f;    // how far from face
    public float moveSpeed = 2f;
    public float DeleteThis;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public CAVE2WandNavigator navigator;
    public Text signatureText;
    private AudioSource penSigning;
    public GameObject typographyObject;   // Assign in Inspector




    void Update()
    {
        UpdateWandOverTimer();
    }
    void Start()
    {
        originalPosition = quad.transform.position;
        originalRotation = quad.transform.rotation;
    }



    new void OnWandButtonDown(CAVE2.WandEvent evt)
    {    
        Debug.Log("Button Pressed: " + evt.button);

        if(evt.button == CAVE2.Button.Button3 && wandPointing && !hasTriggered)
        {
            StartCoroutine(ShowSequence());
        }
    }
    IEnumerator ShowTypography(float originalSpeed, float originalMovementScale)
        {
            hasTriggered = true;


            typographyObject.SetActive(true);

        // Parent FIRST
        //typographyObject.transform.SetParent(playerHead);

        // Reset transform RELATIVE to camera
        //typographyObject.transform.localPosition = new Vector3(0f, -3.15f, 15f);
        //typographyObject.transform.localEulerAngles = new Vector3(0f, -180f, 0f);


        //Vector3 worldPos = typographyObject.transform.position;
        //worldPos.z += 4f;
        //typographyObject.transform.position = worldPos;


        // Now scale it
        //typographyObject.transform.localScale = Vector3.one * 2f;



            yield return new WaitForSeconds(5f); 
      

            typographyObject.SetActive(false);
 
      
    }
    IEnumerator ShowSequence()
    {
        penSigning = quad.GetComponent<AudioSource>();
        float originalTurnSpeed = navigator.turnSpeed;
        float originalMovementScale = navigator.movementScale;

        navigator.turnSpeed = 0f;
        navigator.movementScale = 0f;

        Vector3 facePosition = playerHead.position + playerHead.forward * faceDistance;
        Quaternion faceRotation = Quaternion.LookRotation(playerHead.forward);

        // Move paper in front of the player
        yield return StartCoroutine(MoveQuad(facePosition, faceRotation));
        string text = "John Adams";
        signatureText.text = ""; // clear before writing

        penSigning.Play();
        // Gradually "write" the signature
        foreach (char c in text)
        {
            signatureText.text += c;


            // Play pen sound here if you want
            // AudioSource.PlayClipAtPoint(penSound, playerHead.position);

            yield return new WaitForSeconds(0.2f); // adjust speed
        }
        penSigning.Stop();

        yield return new WaitForSeconds(1f); // small pause at the end


        // Move back 
        yield return StartCoroutine(MoveQuad(originalPosition, originalRotation));

        yield return StartCoroutine(ShowTypography(originalTurnSpeed, originalMovementScale));
        yield return new WaitForSeconds(5f);
        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;

        quad.SetActive(false);

        hasTriggered = true;
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
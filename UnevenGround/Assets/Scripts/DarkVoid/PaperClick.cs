using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI; // old

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
    private AudioSource audioSource;

    public AudioClip penSigning;
    public AudioClip paperSound;
    public AudioClip hmmSound;


    public GameObject typographyObject;   // Assign in Inspector



    private Material mat;
    public Renderer targetRenderer;
    public Color originalColor;


    public Color glowColor = Color.blue;
    public float intensity = 3f;
    public float pulseDuration = 1f; // time in seconds for one pulse phase
    private float timer = 0f;
    private bool glowing = false;

    public Transform target;         // assign VR camera (Main Camera)


    public GameObject leaveRegistrarPortal;
    void Update()
    {


        timer += Time.deltaTime;

        if (!hasTriggered)
        {
            if (timer >= pulseDuration)
            {
                timer = 0f;
                glowing = !glowing; // toggle state
            }

            if (glowing)
            {
                // glowing state: black base + blue emission
                mat.color = Color.black;
                mat.SetColor("_EmissionColor", glowColor * intensity);
            }
            else
            {
                // normal state: white base, no emission
                mat.color = Color.white;
                mat.SetColor("_EmissionColor", Color.black);
            }
        }

      }




    void Start()
    {
        originalPosition = quad.transform.position;
        originalRotation = quad.transform.rotation;

        mat = targetRenderer.material;
        mat.EnableKeyword("_EMISSION");

        mat.color = Color.white;
        mat.SetColor("_EmissionColor", Color.black);


    }



    new void OnWandButtonDown(CAVE2.WandEvent evt)
    {    
        Debug.Log("Button Pressed: " + evt.button);

        if(evt.button == CAVE2.Button.Button3 && wandPointing && !hasTriggered)
        {
            glowing = false;
            mat.color = Color.white;
            mat.SetColor("_EmissionColor", Color.black);

            StartCoroutine(ShowSequence());
        }
    }
    IEnumerator ShowTypography(float originalSpeed, float originalMovementScale)
        {




            typographyObject.SetActive(true);
            audioSource.clip = hmmSound;
            audioSource.Play();


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



            yield return new WaitForSeconds(3f); 
      

            typographyObject.SetActive(false);



    }
    IEnumerator ShowSequence()
    {
        hasTriggered = true;


        audioSource = quad.GetComponent<AudioSource>();
        audioSource.loop = false;

        float originalTurnSpeed = navigator.turnSpeed;
        float originalMovementScale = navigator.movementScale;

        navigator.turnSpeed = 0f;
        navigator.movementScale = 0f;

        Vector3 targetPos = playerHead.position + playerHead.forward * faceDistance;
        Quaternion faceRotation = Quaternion.LookRotation(playerHead.forward);

        // Move paper in front of the player
        yield return StartCoroutine(MoveQuadWRotation(target.position, faceRotation));
        string text = "John Adams";
        signatureText.text = ""; // clear before writing

        audioSource.clip = penSigning;
        audioSource.Play();
        // Gradually "write" the signature
        foreach (char c in text)
        {
            signatureText.text += c;


            // Play pen sound here if you want
            // AudioSource.PlayClipAtPoint(penSound, playerHead.position);

            yield return new WaitForSeconds(0.2f); // adjust speed
        }

        yield return new WaitForSeconds(1f); // small pause at the end


        // Move back 
        yield return StartCoroutine(MoveQuadWRotation(originalPosition, originalRotation));

        yield return StartCoroutine(ShowTypography(originalTurnSpeed, originalMovementScale));
 
        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;

        quad.SetActive(false);

        hasTriggered = true;
        leaveRegistrarPortal.SetActive(true);
    }

    IEnumerator MoveQuadWRotation(Vector3 targetPos, Quaternion targetRot){
        audioSource.clip = paperSound;
        audioSource.Play();
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
using Boo.Lang;
using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.LegacyInputHelpers;

public class registrarFeeSheetTrigger : MonoBehaviour
{
    public GameObject quad;   
    public GameObject petitionQuad;          // assign your quad
    public Transform target;         // assign VR camera (Main Camera)
    public float faceDistance = 1.2f;    // how far from face
    public float moveSpeed = 2f;

    private UnityEngine.Vector3 originalPosition;
    private UnityEngine.Quaternion originalRotation;
    private bool isRunning = false;
    public TextMeshProUGUI feeText;
    public TextMeshProUGUI feeTitleText;
    public CAVE2WandNavigator navigator;

    public GameObject HowDiagonal;

    //public GameObject HowVertical;
    //public GameObject HowHorizontal;

    public GameObject typographyObject_Emotion1;
    public GameObject typographyObject_Emotion2;
    public GameObject typographyObject_Emotion3;
    public GameObject typographyObject_Emotion4;


    private AudioSource audioSource;
    public AudioClip suspenseSoundEffectHow;
    public AudioClip suspenseSoundEffect1;
    public AudioClip suspenseSoundEffect2;
    public AudioClip suspenseSoundEffect3;
    public List<AudioClip> effects;

    public AudioClip breathing;

    public AudioClip paperSound;


    public float speed = 2f;
    public float intensity = 3f;
    private Material mat;

    public GameObject player;
    public Transform playerTarget;

    public Transform cameraControler;
    public Transform cameraControllerTarget;

    void Awake()
    {
        effects = new List<AudioClip>
    {
        suspenseSoundEffect1,
        suspenseSoundEffect2,
        suspenseSoundEffect3
    };
    }

      



    private bool hasPlayed = false;


    void Start()
    {
        originalPosition = quad.transform.position;
        originalRotation = quad.transform.rotation;
        quad.SetActive(false);
        petitionQuad.SetActive(false); // this should be false - Michael

        
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
       

        float originalTurnSpeed = navigator.turnSpeed;
        float originalMovementScale = navigator.movementScale;
        navigator.turnSpeed = 0f;
        navigator.movementScale = 0f;

        yield return StartCoroutine(MovePerson(playerTarget.position, cameraControllerTarget.rotation));





        quad.SetActive(true);

        // Step 1: Wait at desk
        yield return new WaitForSeconds(2f);

        // Step 2: Move to face
        //Vector3 position = playerHead.position + playerHead.forward * faceDistance;
        UnityEngine.Vector3 position = target.position;
        //Quaternion faceRotation = Quaternion.LookRotation(playerHead.forward);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        yield return StartCoroutine(MoveQuad(position));

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
        yield return StartCoroutine(MoveQuad(originalPosition));
        quad.SetActive(false);

        hasPlayed = true;
        petitionQuad.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(ShowEmotionTypography());
        navigator.turnSpeed = originalTurnSpeed;
        navigator.movementScale = originalMovementScale;
        audioSource.clip = breathing;
        audioSource.Play();



    }



    IEnumerator MovePerson(UnityEngine.Vector3 targetPos, UnityEngine.Quaternion targetRot)
    {
        while (UnityEngine.Vector3.Distance(player.transform.position, targetPos) > 0.01f ||
               Mathf.Abs(Mathf.DeltaAngle(cameraControler.transform.eulerAngles.y, targetRot.eulerAngles.y)) > 0.1f)
        {
            player.transform.position = UnityEngine.Vector3.MoveTowards(player.transform.position, targetPos, moveSpeed * Time.deltaTime);
            cameraControler.transform.rotation = UnityEngine.Quaternion.RotateTowards(cameraControler.transform.rotation, targetRot, moveSpeed * 100f * Time.deltaTime);

            yield return null;
        }

        player.transform.position = targetPos;
        cameraControler.transform.rotation = targetRot;
    }

    IEnumerator MoveQuad(UnityEngine.Vector3 targetPos)
    {
        audioSource.clip = paperSound;
        audioSource.Play();
        while (UnityEngine.Vector3.Distance(quad.transform.position, targetPos) > 0.01f)
        {
            quad.transform.position = UnityEngine.Vector3.Lerp(
                quad.transform.position,
                targetPos,
                Time.deltaTime * moveSpeed
            );


            yield return null;
        }

        quad.transform.position = targetPos;
    }
  

    IEnumerator ShowEmotionTypography()
    {

        HowDiagonal.SetActive(false);
        //HowHorizontal.SetActive(false);
        //HowVertical.SetActive(false);



        HowDiagonal.SetActive(true);

        audioSource.clip = suspenseSoundEffectHow;
        audioSource.Play();

        yield return new WaitForSeconds(5f);  // wait before next one
        HowDiagonal.SetActive(false);




        GameObject[] typographyObjects = { typographyObject_Emotion1, typographyObject_Emotion2, typographyObject_Emotion3 }; // assign your 3-4 objects here
        typographyObject_Emotion1.SetActive(false);

        typographyObject_Emotion2.SetActive(false);
        typographyObject_Emotion3.SetActive(false);


        // Reset transform RELATIVE to camera
        //typographyObject_Emotion1.transform.localPosition = new Vector3(8.98f, -26.51f, 14.69f);
        //typographyObject_Emotion1.transform.localEulerAngles = new Vector3(0f, -180f, 0f);


        //typographyObject_Emotion2.transform.localPosition = new Vector3(-10.81f, -39.05f, 14.58f);
        //typographyObject_Emotion2.transform.localEulerAngles = new Vector3(0f, -180f, 0f);

        //typographyObject_Emotion3.transform.localPosition = new Vector3(14.6f, -42.85f, 14.62f);
        //typographyObject_Emotion3.transform.localEulerAngles = new Vector3(0f, -180f, 0f);

        //typographyObject_Emotion4.transform.localPosition = new Vector3(5.15f, -5.26f, 14.51f);
        //typographyObject_Emotion4.transform.localEulerAngles = new Vector3(0f, -180f, 0f);




        int count = 0;
        foreach (GameObject obj in typographyObjects)
        {
   
            obj.SetActive(true);   // show the object
            audioSource.clip = effects[count];
            audioSource.Play();
            yield return new WaitForSeconds(3f);  // wait before next one
            obj.SetActive(false);
            count++;
        }


        // Unparent and hide
        //typographyObject_Emotion1.transform.SetParent(null);
        //typographyObject_Emotion2.transform.SetParent(null);
        //typographyObject_Emotion3.transform.SetParent(null);
        //typographyObject_Emotion4.transform.SetParent(null);


        typographyObject_Emotion1.SetActive(false);
        typographyObject_Emotion2.SetActive(false);
        typographyObject_Emotion3.SetActive(false);



        //typographyObject_Emotion4.SetActive(false);
    }

}
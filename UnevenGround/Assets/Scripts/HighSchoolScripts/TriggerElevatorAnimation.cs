using System.Collections;
using UnityEngine;
public class TriggerElevatorAnimation : MonoBehaviour
{
    LoadNextSceneAsync loadNextSceneAsync;
    AnimatorStateInfo stateInfo;
    public Animator animator;
    // Cache the hash of the bounce state.
    int m_BounceStateHash;
    //public bool shouldPlay = false;
    AudioSource audioSource;
    [SerializeField] float timer = 4f;
    [SerializeField] float timer2 = 4f;
    [SerializeField] GameObject elevatorLight;
    Light elevatorLightComponent;
    float originalMoveScale = 0f;
    CAVE2WandNavigator playerNavigator;
    bool shouldLowerLight = false;
    [SerializeField] float fadeSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        loadNextSceneAsync = FindObjectOfType<LoadNextSceneAsync>();
        elevatorLightComponent = elevatorLight.GetComponent<Light>();
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (elevatorLight == null)
        {
            Debug.LogError($"Elevator Light is missing on GameObject: {gameObject.name}", gameObject);
            return;
        }
        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings <= currentSceneIndex + 1)
        {
            Debug.LogWarning("No more scenes to load.");
            return;
        }

        animator = GetComponent<Animator>();
        animator.enabled = false;
        //m_BounceStateHash = Animator.StringToHash("Base Layer.OpenDoor");
        audioSource = GetComponent<AudioSource>();

        // DarkVoid scene
        if (currentSceneIndex == 1)
        {
            PlayElevatorAnimation();
            PlayElevatorSound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Wand laser interaction, not used (?)
        // check if elevator button is touched by wand laser
        //if (shouldPlay == true)
        //{
        //    shouldPlay = false;
        //    StartCoroutine(ProcessElevatorAnimAudio());
        //}
        if (shouldLowerLight)
        {
            elevatorLightComponent.intensity = Mathf.MoveTowards(elevatorLightComponent.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
        Debug.Log("shouldLowerLight: " + shouldLowerLight);
        Debug.Log("inside?" + loadNextSceneAsync.isPlayerInside);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }
        playerNavigator = other.gameObject.GetComponent<CAVE2WandNavigator>();
        originalMoveScale = playerNavigator.movementScale;

        StartCoroutine(ProcessElevatorAnimAudio(playerNavigator));
    }

    void PlayElevatorAnimation()
    {
        animator.enabled = true;
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("DoorClose"))
        {
            animator.SetTrigger("TriggerOpen");
            //animator.Play(m_BounceStateHash, 0, 0f);
        }

        if (stateInfo.IsName("DoorOpen"))
        {
        }

    }

    void PlayElevatorSound()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.PlayOneShot(audioSource.clip);
    }

    IEnumerator ProcessElevatorAnimAudio(CAVE2WandNavigator playerNavigator)
    {
        playerNavigator.movementScale = 0f;
        PlayElevatorSound();
        yield return new WaitForSeconds(timer);
        PlayElevatorAnimation();
        playerNavigator.movementScale = originalMoveScale;

        Debug.Log("before 11f");
        yield return new WaitForSeconds(11f);
        Debug.Log("after 11f");
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("DoorClose") && loadNextSceneAsync.isPlayerInside)
        {
            Debug.Log("inside IF");
            //door closed, player is inside elevator, play sound and load next scene
            shouldLowerLight = true;
            PlayElevatorSound();
            yield return new WaitForSeconds(timer + 1f);
            loadNextSceneAsync.shouldLoadNextScene = true;
        }

        Debug.Log("OUTSIDE IF");

    }
}

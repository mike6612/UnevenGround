using System.Collections;
using UnityEngine;
public class TriggerElevatorAnimation : MonoBehaviour
{
    GameObject graduationAudioObject;
    AnimatorStateInfo stateInfo;
    public Animator animator;
    // Cache the hash of the bounce state.
    int m_BounceStateHash;
    //public bool shouldPlay = false;
    AudioSource audioSource;
    [SerializeField] AudioSource doorCloseAudio;
    [SerializeField] float timer = 4f;
    [SerializeField] float timer2 = 4f;
    GameObject elevatorLight;
    Light elevatorLightComponent;
    float originalMoveScale = 0f;
    CAVE2WandNavigator playerNavigator;
    bool shouldLowerLight = false;
    [SerializeField] float fadeSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        graduationAudioObject = GameObject.FindGameObjectWithTag("GraduationAudio");
        elevatorLight = GameObject.FindGameObjectWithTag("ElevatorLight");
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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLowerLight)
        {
            elevatorLightComponent.intensity = Mathf.MoveTowards(elevatorLightComponent.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
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

        doorCloseAudio.Play();
        yield return new WaitForSeconds(5.5f);
        doorCloseAudio.Play();


        yield return new WaitForSeconds(11f);
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("DoorClose") && LoadNextSceneAsync.isPlayerInside)
        {
            //door closed, player is inside elevator, play sound and load next scene
            shouldLowerLight = true;
            PlayElevatorSound();
            graduationAudioObject.GetComponent<AudioSource>().Stop();
            yield return new WaitForSeconds(timer + 1f);
            LoadNextSceneAsync.shouldLoadNextScene = true;
        }
    }
}

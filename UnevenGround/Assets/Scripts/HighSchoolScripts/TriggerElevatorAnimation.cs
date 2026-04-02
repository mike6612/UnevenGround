using System.Collections;
using UnityEngine;
public class TriggerElevatorAnimation : MonoBehaviour
{
    Animator animator;
    // Cache the hash of the bounce state.
    int m_BounceStateHash;
    //public bool shouldPlay = false;
    AudioSource audioSource;
    [SerializeField] float timer = 4f;
    float originalMoveScale = 0f;
    CAVE2WandNavigator playerNavigator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        m_BounceStateHash = Animator.StringToHash("Base Layer.OpenDoor");
        audioSource = GetComponent<AudioSource>();
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
        {
            animator.Play(m_BounceStateHash, 0, 0f);
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
    }
}

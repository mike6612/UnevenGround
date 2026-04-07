using System.Collections;
using UnityEngine;

public class ElevatorDarkVoid : MonoBehaviour
{
    public Animator animator;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ProcessElevatorAnimation());

        //PlayElevatorSound();
    }
    void PlayElevatorSound()
    {
        StartCoroutine(ProcessOpenCloseSound());
    }

    // player outside the elevator, resume anim or close the elevator door
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ProcessCloseDoor());
        }
    }

    void PlayElevatorAudio()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.PlayOneShot(audioSource.clip);
    }

    IEnumerator ProcessOpenCloseSound()
    {
        PlayElevatorAudio();
        yield return new WaitForSeconds(5f);
        PlayElevatorAudio();
    }

    IEnumerator ProcessCloseDoor()
    {
        animator.speed = 1f;
        yield return new WaitForSeconds(2f);
        PlayElevatorAudio();
    }

    IEnumerator ProcessElevatorAnimation()
    {
        animator.SetTrigger("TriggerOpen");
        yield return new WaitForSeconds(3.8f);
        animator.speed = 0f;
    }
}

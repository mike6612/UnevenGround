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
        PlayElevatorSound();
        animator.SetTrigger("TriggerOpen");

    }
    void PlayElevatorSound()
    {
        StartCoroutine(ProcessOpenCloseSound());
    }
    // Update is called once per frame
    void Update()
    {

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
}

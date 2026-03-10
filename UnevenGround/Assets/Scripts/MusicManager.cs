using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip poisonTree;
    public AudioClip limerence;
    public AudioClip resonance;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPoisonTree()
    {
        audioSource.Stop();
        audioSource.clip = poisonTree;
        audioSource.Play();
    }

    public void PlayLimerence()
    {
        audioSource.Stop();
        audioSource.clip = limerence;
        audioSource.Play();
    }

    public void PlayResonance()
    {
        audioSource.Stop();
        audioSource.clip = resonance;
        audioSource.Play();
    }
}
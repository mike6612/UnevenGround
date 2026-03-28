using UnityEngine;

public class StomachGrowl : MonoBehaviour
{
    public AudioClip stomachGrowl;

    private AudioSource audioSource;
    private bool played = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (played) return;

        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(stomachGrowl);
            played = true;
        }
    }
}
using UnityEngine;

public class StartMusicTrigger : MonoBehaviour
{
    public MusicManager musicManager;

    private void OnTriggerEnter(Collider other)
    {
        musicManager.PlayPoisonTree();

        // disable trigger after first use
        GetComponent<Collider>().enabled = false;
    }
}
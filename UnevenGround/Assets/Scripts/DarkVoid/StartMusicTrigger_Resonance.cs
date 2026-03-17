using UnityEngine;

public class StartMusicTrigger_Resonance : MonoBehaviour
{
    public MusicManager musicManager;

    private void OnTriggerEnter(Collider other)
    {
        musicManager.PlayResonance();

        // disable trigger after first use
        GetComponent<Collider>().enabled = false;
    }
}
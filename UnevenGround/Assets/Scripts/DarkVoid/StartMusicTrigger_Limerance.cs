using UnityEngine;

public class StartMusicTrigger_Limerance: MonoBehaviour
{
    public MusicManager musicManager;

    private void OnTriggerEnter(Collider other)
    {
        musicManager.PlayLimerence();

        // disable trigger after first use
        GetComponent<Collider>().enabled = false;
    }
}

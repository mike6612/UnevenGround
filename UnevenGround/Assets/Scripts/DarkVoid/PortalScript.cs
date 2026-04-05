using UnityEngine;

public class PortalScript : MonoBehaviour
{

    public GameObject target;
    public GameObject trigger;

    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = target.transform.position;
        player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        trigger.SetActive(true);
        target.SetActive(false);
    }
}
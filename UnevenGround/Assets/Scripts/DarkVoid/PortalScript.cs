using UnityEngine;

public class PortalScript : MonoBehaviour
{

    public GameObject target;
    public GameObject trigger;

    public GameObject player;

    public Transform cameraController;
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = target.transform.position;
        cameraController.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        trigger.SetActive(true);
        target.SetActive(false);
    }
}
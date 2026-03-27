using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    public GameObject target;
    public GameObject trigger;

    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = target.transform.position;
        trigger.SetActive(true);
        target.SetActive(false);
    }
}

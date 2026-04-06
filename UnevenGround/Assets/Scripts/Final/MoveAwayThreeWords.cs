using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAwayThreeWords : MonoBehaviour
{
    public GameObject word;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            Osiciliate osc = word.GetComponent<Osiciliate>();
            if (osc != null)
            {
                osc.TriggerExit();
            }

        }
    }
}
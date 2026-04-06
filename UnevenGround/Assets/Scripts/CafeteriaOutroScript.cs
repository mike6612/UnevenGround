using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class CafeteriaOutroScript : MonoBehaviour
{

    public GameObject foodOutroTypography;
    public GameObject exitPortal;

    void OnTriggerEnter(Collider other)
    {
        Sequence();
    }

    IEnumerator Sequence()
    {
        yield return(ShowTypography());

    }

    IEnumerator ShowTypography()
    {
        foodOutroTypography.SetActive(true);
        yield return new WaitForSeconds(3f);
        exitPortal.SetActive(true);
    }

}




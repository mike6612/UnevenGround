using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTurnOnTrigger : MonoBehaviour
{
    public GameObject light;
    void Start()
    {
        light.SetActive(false);

    }

    void OnTriggerEnter(Collider other)
    {

       light.SetActive(true);
    }




}

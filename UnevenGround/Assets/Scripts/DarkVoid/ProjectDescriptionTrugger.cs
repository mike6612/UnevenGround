using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDescriptionTrugger : MonoBehaviour
{
    public GameObject ProjectDescription;
    void OnTriggerEnter(Collider other)
    {

        ProjectDescription.SetActive(false);
    }

}



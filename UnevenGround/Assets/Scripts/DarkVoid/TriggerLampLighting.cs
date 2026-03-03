using UnityEngine;

public class TriggerLampLighting : MonoBehaviour
{
    [SerializeField] GameObject[] lightsToTurnOn;
    [SerializeField] GameObject[] lightsToTurnOff;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }

        foreach (var light in lightsToTurnOn)
        {
            light.GetComponentInChildren<Light>().enabled = true;
        }

        foreach (var light in lightsToTurnOff)
        {
            light.GetComponentInChildren<Light>().enabled = false;
        }

    }
}

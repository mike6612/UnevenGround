using UnityEngine;

public class ActivateObjectTrigger : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    // Start is called before the first frame update
    void Start()
    {
        objectToActivate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
        }
    }
}

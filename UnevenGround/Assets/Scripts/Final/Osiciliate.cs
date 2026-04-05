using UnityEngine;

public class Osiciliate : MonoBehaviour
{
    //[SerializeField] Vector3 endPosition;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float distanceToMove = 1f;
    Vector3 startPosition;
    Vector3 endPosition;
    float lowerUpperValue = 0f;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition;
    }

    void Update()
    {
        ProcessOsiciliation();
    }

    void ProcessOsiciliation()
    {
        lowerUpperValue = Mathf.PingPong(Time.time * moveSpeed, 1);
        endPosition += distanceToMove * Vector3.up;
        transform.position = Vector3.Lerp(startPosition, endPosition, lowerUpperValue);
    }
}

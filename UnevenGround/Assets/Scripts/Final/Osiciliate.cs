using UnityEngine;

public class Osiciliate : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float distanceToMove = 1f;

    Vector3 startPosition;
    Vector3 endPosition;

    float lowerUpperValue = 0f;

    bool isOscillating = true;
    bool isMovingUp = false;

    float t = 0f;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + (Vector3.up * distanceToMove);
    }

    void Update()
    {
        if (isOscillating)
        {
            ProcessOscillation();
        }
        else if (isMovingUp)
        {
            MoveUpAndDisappear();
        }
    }

    void ProcessOscillation()
    {
        lowerUpperValue = Mathf.PingPong(Time.time * moveSpeed, 1);
        transform.position = Vector3.Lerp(startPosition, endPosition, lowerUpperValue);
    }

    void MoveUpAndDisappear()
    {
        t += Time.deltaTime * moveSpeed;

        float smoothT = Mathf.SmoothStep(0f, 1f, t);
        transform.position = Vector3.Lerp(transform.position, endPosition + Vector3.up * 10f, smoothT);

        if (t >= 1f)
        {
            isMovingUp = false;
            gameObject.SetActive(false); 
        }
    }

    public void TriggerExit()
    {
        isOscillating = false;
        isMovingUp = true;
        t = 0f;
    }

}

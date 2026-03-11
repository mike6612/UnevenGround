using UnityEngine;

public class MoveTowardsPosition : MonoBehaviour
{
    bool shouldMoveTowardsPosition = false;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] Vector3 targetPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        shouldMoveTowardsPosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveTowardsPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        if (transform.position == targetPosition)
        {
            shouldMoveTowardsPosition = false;
        }

    }
}

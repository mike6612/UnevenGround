using UnityEngine;

public class MoveTowardsPosition : MonoBehaviour
{
    bool shouldMoveTowardsPosition = false;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] Vector3 targetPosition = Vector3.zero;
    GameObject player;
    CAVE2WandNavigator playerWandNavigator;
    float originalMovementScale;
    float originalTurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        shouldMoveTowardsPosition = true;
        player = GameObject.FindGameObjectWithTag("Player");
        playerWandNavigator = player.GetComponent<CAVE2WandNavigator>();

        originalMovementScale = playerWandNavigator.movementScale;
        originalTurnSpeed = playerWandNavigator.turnSpeed;

        playerWandNavigator.movementScale = 0;
        playerWandNavigator.turnSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveTowardsPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            shouldMoveTowardsPosition = false;
            playerWandNavigator.movementScale = originalMovementScale;
            playerWandNavigator.turnSpeed = originalTurnSpeed;
            Debug.Log("Word Object reached target: Resetting player speed.");
            Destroy(gameObject);
        }

        Debug.Log($"Current speed" + playerWandNavigator.movementScale);
    }
}

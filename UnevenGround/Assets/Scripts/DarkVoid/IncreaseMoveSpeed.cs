using UnityEngine;

public class IncreaseMoveSpeed : MonoBehaviour
{
    [SerializeField] private float speedIncreaseAmount = 0.5f;
    private bool hasTriggered = false; // only trigger once


    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return; // already triggered

        if (other.CompareTag("Player"))
        {
            CAVE2WandNavigator playerWandNavigator = other.GetComponent<CAVE2WandNavigator>();

            if (playerWandNavigator != null)
            {
                playerWandNavigator.movementScale += speedIncreaseAmount;
                hasTriggered = true; // mark as used
                Destroy(gameObject);
            }
        }
    }
}
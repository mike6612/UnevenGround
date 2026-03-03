using UnityEngine;

public class IncreaseMoveSpeed : MonoBehaviour
{
    [SerializeField] private float speedIncreaseAmount = 0.5f;


    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, Vector3.zero);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CAVE2WandNavigator playerWandNavigator = collision.gameObject.GetComponent<CAVE2WandNavigator>();
            if (playerWandNavigator != null)
            {
                playerWandNavigator.movementScale += speedIncreaseAmount;
                Destroy(gameObject);
            }
        }
    }
}

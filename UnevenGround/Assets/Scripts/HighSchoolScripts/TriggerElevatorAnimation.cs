using UnityEngine;

public class TriggerElevatorAnimation : MonoBehaviour
{
    Animator animator;
    // Cache the hash of the bounce state.
    int m_BounceStateHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_BounceStateHash = Animator.StringToHash("Base Layer.OpenDoor");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
        {
            animator.Play(m_BounceStateHash, 0, 0f);
        }

    }

}

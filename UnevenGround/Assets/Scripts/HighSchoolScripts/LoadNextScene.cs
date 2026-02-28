using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings <= currentSceneIndex + 1)
        {
            Debug.LogWarning("No more scenes to load.");
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(++currentSceneIndex);
    }
}

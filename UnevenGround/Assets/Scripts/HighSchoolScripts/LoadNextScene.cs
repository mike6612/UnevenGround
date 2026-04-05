using System.Collections;
using UnityEngine;
public class LoadNextScene : MonoBehaviour
{
    LoadNextSceneAsync loadNextSceneAsync;
    // Start is called before the first frame update
    void Start()
    {
        loadNextSceneAsync = FindObjectOfType<LoadNextSceneAsync>();
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

        // Highschool scene
        if (currentSceneIndex == 0)
        {
            //StartCoroutine(WaitForXSeconds(5));
        }

        //UnityEngine.SceneManagement.SceneManager.LoadScene(++currentSceneIndex);
    }

    IEnumerator WaitForXSeconds(int x)
    {
        yield return new WaitForSeconds(x);
        //loadNextSceneAsync.shouldLoadNextScene = true;
    }
}

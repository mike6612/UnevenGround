using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadFinalSceneAsync : MonoBehaviour
{
    public bool shouldLoadNextScene123 = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenLoad());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }
        shouldLoadNextScene123 = true;
    }

    IEnumerator WaitThenLoad()
    {
        for (int i = 0; i < 5; i++) yield return null;

        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation;
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        asyncOperation = SceneManager.LoadSceneAsync(2);

        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                if (shouldLoadNextScene123 == true)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}

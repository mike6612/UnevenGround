//This script lets you load a Scene asynchronously. It uses an asyncOperation to calculate the progress and outputs the current progress to Text (could also be used to make progress bars).

//Attach this script to a GameObject
//Create a Button (Create>UI>Button) and a Text GameObject (Create>UI>Text) and attach them both to the Inspector of your GameObject
//In Play Mode, press your Button to load the Scene, and the Text changes depending on progress. Press the space key to activate the Scene.
//Note: The progress may look like it goes straight to 100% if your Scene doesn’t have a lot to load.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneAsync : MonoBehaviour
{
    int currentSceneIndex = 0;
    public bool isPlayerInside = false;
    public bool shouldLoadNextScene = false;
    void Start()
    {
        currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings <= currentSceneIndex + 1)
        {
            Debug.LogWarning("No more scenes to load.");
            return;
        }
        StartCoroutine(WaitThenLoad());
    }

    void OnCollisionEnter(Collision collision)
    {
        // safety check
        if (!collision.gameObject.CompareTag("Player")) { return; }
        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings <= currentSceneIndex + 1)
        {
            Debug.LogWarning("No more scenes to load.");
            return;
        }
        isPlayerInside = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }
        isPlayerInside = false;
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(++currentSceneIndex);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                if (shouldLoadNextScene == true)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}

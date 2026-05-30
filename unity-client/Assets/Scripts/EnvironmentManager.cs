using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentManager : MonoBehaviour
{
    private string currentEnvironmentScene;

    public void LoadEnvironment(string environmentSceneName)
    {
        StartCoroutine(LoadEnvironmentRoutine(environmentSceneName));
    }

    private IEnumerator LoadEnvironmentRoutine(string environmentSceneName)
    {
        if (!string.IsNullOrEmpty(currentEnvironmentScene))
        {
            yield return SceneManager.UnloadSceneAsync(currentEnvironmentScene);
        }

        yield return SceneManager.LoadSceneAsync(environmentSceneName, LoadSceneMode.Additive);

        currentEnvironmentScene = environmentSceneName;

        Debug.Log("Loaded environment: " + environmentSceneName);
    }
}
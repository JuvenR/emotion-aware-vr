using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private string defaultEnvironmentScene = "Env_Hospital";
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 playerSpawnPosition = new Vector3(0f, 1f, 0f);

    private string currentEnvironmentScene;

    private void Start()
    {
        LoadEnvironment(defaultEnvironmentScene);
    }

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

        ResetPlayerPosition();

        Debug.Log("Loaded environment: " + environmentSceneName);
    }

    private void ResetPlayerPosition()
    {
        if (player == null)
        {
            return;
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        player.position = playerSpawnPosition;
        player.rotation = Quaternion.identity;
    }
}
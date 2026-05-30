using UnityEngine;

public class EmotionApiClient : MonoBehaviour
{
    public static EmotionApiClient Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SendEvent(EmotionEvent emotionEvent)
    {
        string json = JsonUtility.ToJson(emotionEvent, true);

        Debug.Log("Sending emotion event:");
        Debug.Log(json);
    }
}
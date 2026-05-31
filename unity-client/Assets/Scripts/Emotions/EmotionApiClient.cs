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

        EmotionResponse fakeResponse = new EmotionResponse
        {
            emotion = "anxiety",
            intensity = 0.8f,
            behavior_tag = "avoid_user",
            animation_tag = "nervous_idle",
            environment_effect = "increase_tension"
        };

        HandleResponse(fakeResponse);
    }

    private void HandleResponse(EmotionResponse response)
    {
        Debug.Log("Received emotion response:");
        Debug.Log(JsonUtility.ToJson(response, true));

        NPCEmotionController npc = FindFirstObjectByType<NPCEmotionController>();

        if (npc != null)
        {
            npc.ApplyEmotion(response);
        }
        else
        {
            Debug.LogWarning("No NPCEmotionController found in the current scene.");
        }
    }
}
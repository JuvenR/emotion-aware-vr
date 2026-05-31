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

        EmotionResponse fakeResponse = CreateFakeResponse(emotionEvent);

        HandleResponse(fakeResponse);
    }

    private EmotionResponse CreateFakeResponse(EmotionEvent emotionEvent)
    {
        switch (emotionEvent.event_type)
        {
            case "user_gets_too_close":
                return new EmotionResponse
                {
                    emotion = "anxiety",
                    intensity = 0.8f,
                    behavior_tag = "avoid_user",
                    animation_tag = "nervous_idle",
                    environment_effect = "increase_tension"
                };

            case "user_acts_aggressive":
                return new EmotionResponse
                {
                    emotion = "anger",
                    intensity = 0.85f,
                    behavior_tag = "confront_user",
                    animation_tag = "angry_idle",
                    environment_effect = "warning_tension"
                };

            case "alarm_starts":
                return new EmotionResponse
                {
                    emotion = "fear",
                    intensity = 0.9f,
                    behavior_tag = "step_back",
                    animation_tag = "fear_idle",
                    environment_effect = "panic_tension"
                };

            case "user_helps_npc":
                return new EmotionResponse
                {
                    emotion = "calm",
                    intensity = 0.6f,
                    behavior_tag = "trust_user",
                    animation_tag = "relaxed_idle",
                    environment_effect = "reduce_tension"
                };

            default:
                return new EmotionResponse
                {
                    emotion = "neutral",
                    intensity = 0.3f,
                    behavior_tag = "idle",
                    animation_tag = "neutral_idle",
                    environment_effect = "none"
                };
        }
    }

    private void HandleResponse(EmotionResponse response)
    {
        Debug.Log("Received emotion response:");
        Debug.Log(JsonUtility.ToJson(response, true));

        NPCEmotionController npc = FindAnyObjectByType<NPCEmotionController>();

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
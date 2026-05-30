using System;
using UnityEngine;

public class EmotionTrigger : MonoBehaviour
{
    [Header("Emotion Event Data")]
    [SerializeField] private string environmentId;
    [SerializeField] private string scenarioId;
    [SerializeField] private string eventType;
    [SerializeField] private string targetId;
    [SerializeField] private float intensity = 1f;

    [Header("Trigger Settings")]
    [SerializeField] private float cooldownSeconds = 3f;

    private float lastTriggeredTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (Time.time - lastTriggeredTime < cooldownSeconds)
        {
            return;
        }

        lastTriggeredTime = Time.time;

        EmotionEvent emotionEvent = new EmotionEvent
        {
            environment_id = environmentId,
            scenario_id = scenarioId,
            event_type = eventType,
            actor_id = "player",
            target_id = targetId,
            intensity = intensity,
            timestamp = DateTime.UtcNow.ToString("o")
        };

        EmotionApiClient.Instance.SendEvent(emotionEvent);
    }
}
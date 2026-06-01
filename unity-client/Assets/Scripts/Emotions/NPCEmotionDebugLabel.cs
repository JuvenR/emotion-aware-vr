using TMPro;
using UnityEngine;

public class NPCEmotionDebugLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text labelText;

    private void Awake()
    {
        if (labelText == null)
        {
            labelText = GetComponentInChildren<TMP_Text>();
        }
    }

    public void UpdateLabel(EmotionResponse response, Color emotionColor)
    {
        if (labelText == null)
        {
            Debug.LogWarning("NPCEmotionDebugLabel has no TMP_Text assigned.");
            return;
        }

        labelText.text =
            $"Emotion: {response.emotion}\n" +
            $"Intensity: {response.intensity:0.00}\n" +
            $"Behavior: {response.behavior_tag}";

        labelText.color = emotionColor;

        Debug.Log("Debug label updated: " + response.emotion);
    }
}
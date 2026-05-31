using UnityEngine;

public class EnvironmentEmotionController : MonoBehaviour
{
    [Header("Environment Lighting")]
    [SerializeField] private Light[] environmentLights;

    [Header("Emotion Light Colors")]
    [SerializeField] private Color neutralColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color anxietyColor = new Color(0.2f, 0.45f, 1f);
    [SerializeField] private Color fearColor = new Color(0.05f, 0.05f, 0.5f);
    [SerializeField] private Color angerColor = new Color(1f, 0.1f, 0.05f);
    [SerializeField] private Color calmColor = new Color(0.4f, 1f, 0.5f);

    [Header("Intensity")]
    [SerializeField] private float neutralIntensity = 1.2f;
    [SerializeField] private float anxietyIntensity = 0.8f;
    [SerializeField] private float fearIntensity = 0.5f;
    [SerializeField] private float angerIntensity = 1.6f;
    [SerializeField] private float calmIntensity = 1.0f;

    private void Awake()
    {
        if (environmentLights == null || environmentLights.Length == 0)
        {
            environmentLights = FindObjectsByType<Light>();
        }
    }

    public void ApplyEmotion(EmotionResponse response)
    {
        Color color = GetColorForEmotion(response.emotion);
        float intensity = GetIntensityForEmotion(response.emotion);

        Debug.Log("Environment emotion applied: " + response.emotion);
        Debug.Log("Environment lights count: " + environmentLights.Length);

        foreach (Light light in environmentLights)
        {
            Debug.Log("Changing light: " + light.name);

            light.color = color;
            light.intensity = intensity;
        }

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = color * 0.35f;
    }

    private Color GetColorForEmotion(string emotion)
    {
        switch (emotion.ToLower())
        {
            case "anxiety":
                return anxietyColor;

            case "fear":
                return fearColor;

            case "anger":
                return angerColor;

            case "calm":
                return calmColor;

            case "neutral":
            default:
                return neutralColor;
        }
    }

    private float GetIntensityForEmotion(string emotion)
    {
        switch (emotion.ToLower())
        {
            case "anxiety":
                return anxietyIntensity;

            case "fear":
                return fearIntensity;

            case "anger":
                return angerIntensity;

            case "calm":
                return calmIntensity;

            case "neutral":
            default:
                return neutralIntensity;
        }
    }
}
using UnityEngine;

public class NPCEmotionController : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private Renderer[] targetRenderers;

    [Header("Material Filters")]
    [SerializeField] private string[] glowMaterialKeywords = { "Glow", "Eyes","Gold_Primary" };

    [Header("Emotion Colors")]
    [SerializeField] private Color neutralColor = new Color(0.39f, 0.96f, 0.68f);  
    [SerializeField] private Color calmColor = new Color(0.39f, 0.96f, 0.68f);      // green / calm
    [SerializeField] private Color angerColor = new Color(1f, 0.23f, 0.19f);        // red / anger
    [SerializeField] private Color anxietyColor = new Color(1f, 0.72f, 0.01f);      // amber / anxiety
    [SerializeField] private Color fearColor = new Color(0.43f, 0.36f, 0.96f);      // indigo-purple / fear

    [Header("Debug Label")]
    [SerializeField] private NPCEmotionDebugLabel debugLabel;

    [Header("Emission")]
    [SerializeField] private float emissionIntensity = 0.3f;
    private void Awake()
    {
        if (targetRenderers == null || targetRenderers.Length == 0)
        {
            targetRenderers = GetComponentsInChildren<Renderer>();
        }

        if (debugLabel == null)
        {
            debugLabel = GetComponentInChildren<NPCEmotionDebugLabel>();
        }
    }

   public void ApplyEmotion(EmotionResponse response)
    {
        Color emotionColor = GetColorForEmotion(response.emotion);

        ApplyGlowColor(emotionColor);

        if (debugLabel != null)
        {
            debugLabel.UpdateLabel(response, emotionColor);
        }
        else
        {
            Debug.LogWarning("NPCEmotionController has no debug label assigned.");
        }

        Debug.Log("NPC emotion applied: " + response.emotion);
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

    private void ApplyGlowColor(Color color)
    {
        foreach (Renderer targetRenderer in targetRenderers)
        {
            Material[] materials = targetRenderer.materials;

            foreach (Material material in materials)
            {
                if (!ShouldAffectMaterial(material.name))
                {
                    continue;
                }

                if (material.HasProperty("_BaseColor"))
                {
                    material.SetColor("_BaseColor", color);
                }

                if (material.HasProperty("_Color"))
                {
                    material.SetColor("_Color", color);
                }

                if (material.HasProperty("_EmissionColor"))
                {
                    material.EnableKeyword("_EMISSION");
                    Color emissionColor = new Color(
                        color.r * emissionIntensity,
                        color.g * emissionIntensity,
                        color.b * emissionIntensity,
                        1f
                    );

                    material.SetColor("_EmissionColor", emissionColor);                }
            }
        }
    }

    private bool ShouldAffectMaterial(string materialName)
    {
        foreach (string keyword in glowMaterialKeywords)
        {
            if (materialName.ToLower().Contains(keyword.ToLower()))
            {
                return true;
            }
        }

        return false;
    }
}
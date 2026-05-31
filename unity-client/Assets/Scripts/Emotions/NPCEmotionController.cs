using UnityEngine;

public class NPCEmotionController : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private Renderer[] targetRenderers;

    [Header("Material Filters")]
    [SerializeField] private string[] glowMaterialKeywords = { "Glow", "Eyes" };

    [Header("Emotion Colors")]
    [SerializeField] private Color neutralColor = Color.green;
    [SerializeField] private Color anxietyColor = Color.cyan;
    [SerializeField] private Color fearColor = new Color(0.6f, 0.8f, 1f);
    [SerializeField] private Color angerColor = Color.red;
    [SerializeField] private Color calmColor = new Color(0.4f, 1f, 0.4f);

    [Header("Emission")]
    [SerializeField] private float emissionIntensity = 2.5f;

    private void Awake()
    {
        if (targetRenderers == null || targetRenderers.Length == 0)
        {
            targetRenderers = GetComponentsInChildren<Renderer>();
        }
    }

    public void ApplyEmotion(EmotionResponse response)
    {
        Color emotionColor = GetColorForEmotion(response.emotion);

        ApplyGlowColor(emotionColor);

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
                    material.SetColor("_EmissionColor", color * emissionIntensity);
                }
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
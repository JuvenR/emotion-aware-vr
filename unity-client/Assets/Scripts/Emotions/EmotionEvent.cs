using System;

[Serializable]
public class EmotionEvent
{
    public string environment_id;
    public string scenario_id;
    public string event_type;
    public string actor_id;
    public string target_id;
    public float intensity;
    public string timestamp;
}
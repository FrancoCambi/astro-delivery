using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level Info", fileName = "Level Info")]
public class LevelInfo : ScriptableObject
{
    [Header("Timing")]
    [SerializeField] private float specialTimeReference;
    [SerializeField] private float specialTimeMultiplier;
    [SerializeField] private float normalTimeReference;
    [SerializeField] private float normalTimeMultiplier;

    [Header("Audio")]
    public AudioClip MusicClip;

    public float SpecialTimeCap { get { return specialTimeReference * specialTimeMultiplier; }}
    public float NormalTimeCap { get { return normalTimeReference * normalTimeMultiplier; } }

}

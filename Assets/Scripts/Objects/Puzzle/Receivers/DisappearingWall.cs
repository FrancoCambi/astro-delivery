using UnityEngine;

public class DisappearingWall : PuzzleReceiver
{
    [Header("DW References")]
    [SerializeField] private GameObject wall;

    [Header("DW Settings")]
    [SerializeField] private bool actuateOnContrary;
    [SerializeField] private bool activatedByDefault;

    public override void Actuate()
    { 
        bool arg = actuateOnContrary ? !wall.activeSelf : !activatedByDefault;
        wall.SetActive(arg);
    }

    public override void Restore()
    {
        bool arg = actuateOnContrary ? !wall.activeSelf : activatedByDefault;
        wall.SetActive(arg);
    }
}

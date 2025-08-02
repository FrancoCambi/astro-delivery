using UnityEngine;

public class DisappearingWall : PuzzleReceiver
{
    [Header("References")]
    [SerializeField] private GameObject wall;

    public override void Actuate()
    {
        wall.SetActive(!wall.activeSelf);
    }

    public override void Restore()
    {
        wall.SetActive(!wall.activeSelf);
    }
}

using UnityEngine;

public class Lever : PuzzleSender
{
    [Header("Lever References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Sprite spriteMiddle;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    [Header("Lever Settings")]
    [SerializeField] private bool directionSensitive;
    [SerializeField] private float maxPlayerDistance;

    protected override void Update()
    {
        base.Update();

        if (CanBeUsed())
        {
            if (Input.GetKeyDown(GameController.Instance.InteractKey))
            {
                Use();
            }
        }
    }

    private void Use()
    {
        if (!used)
        {
            Activate();
        }
        else
        {
            DeActivate();
        }
    }

    private bool CanBeUsed()
    {
        return Vector2.Distance(transform.position, player.transform.position) < maxPlayerDistance;
    }


}

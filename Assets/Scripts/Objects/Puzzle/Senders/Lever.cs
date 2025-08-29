using UnityEngine;

public class Lever : PuzzleSender
{
    [Header("Lever References")]
    [SerializeField] private Sprite spriteMiddle;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    [Header("Lever Settings")]
    [SerializeField] private bool directionSensitive;
    [SerializeField] private PuzzleReceiver[] leftReceivers;
    [SerializeField] private PuzzleReceiver[] rightReceivers;
    [SerializeField] private float maxPlayerDistance;

    private GameObject player;
    // 0 -> middle, -1 -> left, 1 -> right
    private int currentStatus = 0; 

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

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
        if (!directionSensitive) NormalUse();
        else DirectionUse();
    }

    private void NormalUse()
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

    private void DirectionUse()
    {
        if (player.transform.position.x <= transform.position.x)
        {
            if (currentStatus == 0 || currentStatus == 1)
            {
                if (currentStatus != 0) DeActivateRight();
                ActivateLeft();
                currentStatus = -1;
            }
        }
        else
        {
            if (currentStatus == 0 || currentStatus == -1)
            {
                if (currentStatus != 0) DeActivateLeft();
                ActivateRight();
                currentStatus = 1;
            }
        }
    }

    private void ActivateLeft()
    {
        foreach (PuzzleReceiver receiver in leftReceivers)
        {
            receiver.ReceiveActivation();
        }
        if (spriteLeft) spriteRenderer.sprite = spriteLeft;
        timeAfterPressed = 0f;

        if (activateClip) SoundFXManager.Instance.PlaySoundFXClip(activateClip, transform);
    }

    private void ActivateRight()
    {

        foreach (PuzzleReceiver receiver in rightReceivers)
        {
            receiver.ReceiveActivation();
        }
        if (spriteRight) spriteRenderer.sprite = spriteRight;
        timeAfterPressed = 0f;

        if (activateClip) SoundFXManager.Instance.PlaySoundFXClip(activateClip, transform);
    }

    private void DeActivateLeft()
    {
        foreach (PuzzleReceiver receiver in leftReceivers)
        {
            receiver.ReceiveDeactivation();
        }
    }

    private void DeActivateRight()
    {
        foreach (PuzzleReceiver receiver in rightReceivers)
        {
            receiver.ReceiveDeactivation();
        }
    }

    private bool CanBeUsed()
    {
        return Vector2.Distance(transform.position, player.transform.position) < maxPlayerDistance;
    }


}

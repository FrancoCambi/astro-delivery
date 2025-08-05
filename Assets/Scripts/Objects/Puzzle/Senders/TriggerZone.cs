using UnityEngine;

public class TriggerZone : PuzzleSender
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used) Activate();
    }
}

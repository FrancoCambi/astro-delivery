using System.Collections;
using UnityEngine;

public class PressureButton : PuzzleSender
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used) Activate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timeAfterPressed = 0;
    }

}

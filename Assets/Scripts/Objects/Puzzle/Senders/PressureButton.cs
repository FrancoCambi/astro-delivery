using Steamworks.Data;
using System.Collections;
using UnityEngine;

public class PressureButton : PuzzleSender
{
    [Header("Button Settings")]
    [SerializeField] private bool onlyPressure = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used) Activate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onlyPressure)
        {
            DeActivate();
        }
    }

}

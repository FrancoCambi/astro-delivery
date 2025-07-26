using System.Collections;
using UnityEngine;

public class PressureButton : PuzzleSender
{
    [Header("References")]
    [SerializeField] private Sprite spriteUnpressed;
    [SerializeField] private Sprite spritePressed;

    private SpriteRenderer spriteRenderer;

    private bool pressed = false;
    private float timeAfterPressed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (releaseAfterTime && pressed)
        {
            timeAfterPressed += Time.deltaTime;

            if (timeAfterPressed >= releaseTime)
            {
                Release();
                timeAfterPressed = 0;
            }
        }
    }

    private void Press()
    {
        pressed = true;
        Activate();
        spriteRenderer.sprite = spritePressed;
    }

    private void Release()
    {
        pressed = false;
        DeActivate();
        spriteRenderer.sprite = spriteUnpressed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pressed) Press();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timeAfterPressed = 0;
    }

}

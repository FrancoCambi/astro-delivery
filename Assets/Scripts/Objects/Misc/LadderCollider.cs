using Unity.VisualScripting;
using UnityEngine;

public class LadderCollider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private new BoxCollider2D collider;

    [Header("Settings")]
    [SerializeField] private LayerMask playerLayer;

    private readonly float checkDistance = 0.05f;
    private bool wantToDrop = false;
    private bool playerOnTop = false;

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        HandlePlatformEffect();
        HandleDropDown();
    }

    private void CheckInput()
    {
        if (Input.GetButton("Down"))
        {
            wantToDrop = true;   
        }
    }

    private void HandlePlatformEffect()
    {
        RaycastHit2D upHit = Physics2D.BoxCast(collider.bounds.center + new Vector3(0, 1), 
            collider.size, 0, Vector2.up, checkDistance, playerLayer);
        RaycastHit2D downHit = Physics2D.BoxCast(collider.bounds.center - new Vector3(0, 1), 
            collider.size, 0, Vector2.down, checkDistance, playerLayer);

        if (upHit)
        {
            MakeCollidable();
            playerOnTop = true;
        }
        else
        {
            playerOnTop = false;
        }

        if (downHit)
        {
            MakePassable();
        }
    }

    private void MakePassable()
    {
        collider.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void MakeCollidable()
    {
        collider.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void HandleDropDown()
    {
        if (wantToDrop && playerOnTop)
        {
            DropDown();
            wantToDrop = false;
        }
    }

    private void DropDown()
    {
        MakePassable();
    }
}

using UnityEngine;

public enum PlayerState
{
    None, Idle, Walk, Jump, Duck, Hit, Climb
}

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    public PlayerState State { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (State == PlayerState.Idle)
        {
            animator.Play("idle");
        }
        else if (State == PlayerState.Walk)
        {
            animator.Play("walk");
        }
        else if (State == PlayerState.Jump)
        {
            animator.Play("jump");
        }
        else if (State == PlayerState.Duck)
        {
            animator.Play("duck");
        }
        else if (State == PlayerState.Hit)
        {
            animator.Play("hit");
        }
        else if (State == PlayerState.Climb)
        {
            animator.Play("climb");
        }
        else
        {
            return;
        }
    }
}

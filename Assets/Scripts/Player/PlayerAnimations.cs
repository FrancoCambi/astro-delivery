using UnityEngine;

public enum PlayerAnimationState
{
    None, Idle, Walk, Jump, Duck, Hit, Climb
}

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    public PlayerAnimationState State { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (State == PlayerAnimationState.Idle)
        {
            animator.Play("idle");
        }
        else if (State == PlayerAnimationState.Walk)
        {
            animator.Play("walk");
        }
        else if (State == PlayerAnimationState.Jump)
        {
            animator.Play("jump");
        }
        else if (State == PlayerAnimationState.Duck)
        {
            animator.Play("duck");
        }
        else if (State == PlayerAnimationState.Hit)
        {
            animator.Play("hit");
        }
        else if (State == PlayerAnimationState.Climb)
        {
            animator.Play("climb");
        }
        else
        {
            return;
        }
    }

    public void Stop()
    {
        animator.speed = 0f;
    }

    public void Resume()
    {
        animator.speed = 1f;
    }
}

using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [Header("Colliders")]
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private BoxCollider2D crouchingCollider;
    [SerializeField] private BoxCollider2D climbingCollider;
    [SerializeField] private BoxCollider2D carryingCollider;

    public BoxCollider2D CurrentCollider { get; private set; }

    private void Awake()
    {
        CurrentCollider = normalCollider;
    }

    private void OnEnable()
    {
        PlayerStateController.OnStateChanged += UpdateCollider;
    }

    private void OnDisable()
    {
        PlayerStateController.OnStateChanged -= UpdateCollider;

    }

    private void UpdateCollider(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Normal:
                CurrentCollider.enabled = false;
                CurrentCollider = normalCollider;
                CurrentCollider.enabled = true;
                break;
            case PlayerState.Crouching:
                CurrentCollider.enabled = false;
                CurrentCollider = crouchingCollider;
                CurrentCollider.enabled = true;
                break;
            case PlayerState.Climbing:
                CurrentCollider.enabled = false;
                CurrentCollider = climbingCollider;
                CurrentCollider.enabled = true;
                break;
            case PlayerState.Carrying:
                CurrentCollider.enabled = false;
                CurrentCollider = carryingCollider;
                CurrentCollider.enabled = true;
                break;
            default:
                break;


        }
    }
}

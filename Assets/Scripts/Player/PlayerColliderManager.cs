using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [Header("Colliders")]
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private BoxCollider2D crouchingCollider;
    [SerializeField] private BoxCollider2D climbingCollider;
    [SerializeField] private BoxCollider2D carryingPackageCollider;

    public BoxCollider2D NormalCollider
    {
        get
        {
            return normalCollider;
        }
    }
    public BoxCollider2D CarryingPackageCollider
    {
        get
        {
            return carryingPackageCollider;
        }
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
                carryingPackageCollider.enabled = false;
                break;
            case PlayerState.Carrying:
                carryingPackageCollider.enabled = true;
                break;
            default:
                break;


        }
    }

}

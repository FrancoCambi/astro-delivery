using System;
using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    public static event Action OnShouldDropPackage;

    private PlayerStateController playerState;

    private void OnEnable()
    {
        OnShouldDropPackage += DropPackage;
    }
    private void Start()
    {
        playerState = GetComponent<PlayerStateController>();
    }

    private void OnDisable()
    {
        OnShouldDropPackage -= DropPackage;

    }

    public static void RequestDrop()
    {
        OnShouldDropPackage?.Invoke();
    }
    private void GrabPackage()
    {
        Package.Instance.HeldStart();
        playerState.SetState(PlayerState.Carrying);
    }

    private void DropPackage()
    {
        Package.Instance.HeldStop();
        playerState.SetState(PlayerState.Normal);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Package"))
        {
            GrabPackage();
        }
    }
}

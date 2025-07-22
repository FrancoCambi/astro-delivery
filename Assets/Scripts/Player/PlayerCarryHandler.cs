using System;
using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float tooltipDistance;

    #region events
    public static event Action OnShouldDropPackage;
    #endregion

    #region attributes
    private PlayerStateController playerState;
    #endregion

    private void OnEnable()
    {
        OnShouldDropPackage += DropPackage;
    }
    private void Start()
    {
        playerState = GetComponent<PlayerStateController>();
    }

    private void Update()
    {
        if (CanGrabPackage())
        {
            // Show

            if (Input.GetKeyDown(KeyCode.E))
            {
                GrabPackage();
                return;
            }
        }

        if (Package.Instance.IsBeingHeld && Input.GetKeyDown(KeyCode.E))
        {
            DropPackage();
        }
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

    private bool CanGrabPackage()
    {
        return !Package.Instance.IsBeingHeld && 
            Vector3.Distance(transform.position, Package.Instance.transform.position) <= tooltipDistance; 
    }
    
}

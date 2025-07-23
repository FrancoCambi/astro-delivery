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
        OnShouldDropPackage += HardDropPackage;
    }
    private void Start()
    {
        playerState = GetComponent<PlayerStateController>();
    }

    private void Update()
    {
        if (CanGrabPackage())
        {
            Tooltip.Instance.ShowTooltip("Use E to pickup the package.");

            if (Input.GetKeyDown(KeyCode.E))
            {
                GrabPackage();
                Tooltip.Instance.HideTooltip();
                return;
            }
        }
        else
        {
            Tooltip.Instance.HideTooltip();
        }

        if (Package.Instance.IsBeingHeld && Input.GetKeyDown(KeyCode.E))
        {
            SoftDropPackage();
        }
    }

    private void OnDisable()
    {
        OnShouldDropPackage -= HardDropPackage;

    }

    public static void RequestHardDrop()
    {
        OnShouldDropPackage?.Invoke();
    }
    private void GrabPackage()
    {
        Package.Instance.Grab();
        playerState.SetState(PlayerState.Carrying);
    }

    private void HardDropPackage()
    {
        Package.Instance.HardDrop();
        playerState.SetState(PlayerState.Normal);
    }

    private void SoftDropPackage()
    {
        Package.Instance.SoftDrop();
        playerState.SetState(PlayerState.Normal);
    }

    private bool CanGrabPackage()
    {
        return !Package.Instance.IsBeingHeld && 
            Vector3.Distance(transform.position, Package.Instance.transform.position) <= tooltipDistance; 
    }
    
}

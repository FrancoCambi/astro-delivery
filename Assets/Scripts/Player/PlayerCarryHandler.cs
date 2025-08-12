using System;
using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float tooltipDistance;

    private PlayerStateController playerState;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private bool calculatingTime = false;
    private float fallingTime;

    public bool IsCarrying => Package.Instance.IsBeingHeld;

    public static event Action OnShouldDropPackage;

    private void OnEnable()
    {
        OnShouldDropPackage += HardDropPackage;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerState = GetComponent<PlayerStateController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (CanGrabPackage())
        {

            if (Input.GetKeyDown(GameController.Instance.InteractKey))
            {
                GrabPackage();
                return;
            }
        }

        if (Package.Instance.IsBeingHeld && Input.GetKeyDown(KeyCode.F))
        {
            SoftDropPackage();
        }

        CheckFalling();

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

    public void HardDropPackage()
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

    public void CheckFalling()
    {
        if (!IsCarrying) return;

        if (playerMovement.Falling && !calculatingTime)
        {
            calculatingTime = true;
        }
        else if (playerMovement.Falling && calculatingTime)
        {
            fallingTime += Time.deltaTime;
        }
        else
        {
            if (fallingTime >= 0.58f)
            {
                HardDropPackage();
            }
            calculatingTime = false;
            fallingTime = 0f;
        }
    }

}

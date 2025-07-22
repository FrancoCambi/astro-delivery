using System;
using UnityEngine;

public enum PlayerState { Normal, Crouching, Climbing, Carrying }
public class PlayerStateController : MonoBehaviour
{
    public static event Action<PlayerState> OnStateChanged;
    public PlayerState CurrentState { get; private set; }

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKeyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip collectKeyClip;

    private bool hasKey = false;

    public static Action OnKeyCollected;
    public static Action OnKeyConsumed;

    public bool HasKey
    {
        get
        {
            return hasKey;
        }
    }

    private void CollectKey()
    {
        hasKey = true;
        OnKeyCollected?.Invoke();
        SoundFXManager.Instance.PlaySoundFXClip(collectKeyClip, transform);
    }

    public void ConsumeKey()
    {
        hasKey = false;
        OnKeyConsumed?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasKey && collision.gameObject.CompareTag("Key"))
        {
            CollectKey();
            Destroy(collision.gameObject);
        }
    }
}

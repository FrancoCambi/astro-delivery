using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 newPos;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mainCamera.transform.position = newPos;
    }
}

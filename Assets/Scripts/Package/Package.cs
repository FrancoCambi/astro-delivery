using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Package : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Vector2 offset;

    private Rigidbody2D rb;

    private bool held;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        held = false;
    }

    #region management

    private void HeldStart()
    {
        held = true;
        PlayerManager.Instance.HoldingPackage = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
        transform.SetParent(PlayerManager.Instance.transform);
        transform.localPosition = offset;
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    private void HeldStop()
    {
        held = false;
        PlayerManager.Instance.HoldingPackage = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;

    }

    #endregion

    #region collisions

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeldStart();
        }
    }


    #endregion
}

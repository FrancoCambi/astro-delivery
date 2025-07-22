using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Package : MonoBehaviour
{
    private static Package instance;

    public static Package Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Position")]
    [SerializeField] private Vector2 offset;

    [Header("Misc")]
    [SerializeField] private GameObject prefab;

    private Rigidbody2D rb;
    private new BoxCollider2D collider;
    private GameObject player;

    private Vector3 startingPos;

    public bool IsBeingHeld { get; private set; }
   
    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        startingPos = transform.position;

        IsBeingHeld = false;
    }

    private void Start()
    {
        // Inefficient, i know.
        // Does not matter given the size of the game. For now.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    #region management

    public void HeldStart()
    {
        transform.rotation = Quaternion.identity;
        transform.SetParent(player.transform);
        gameObject.layer = LayerMask.NameToLayer("Player");
        transform.localPosition = offset;
        rb.bodyType = RigidbodyType2D.Kinematic;
        IsBeingHeld = true;

    }

    public void HeldStop()
    {
        collider.isTrigger = true;
        transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        IsBeingHeld = false;

    }

    private void Respawn()
    {
        gameObject.SetActive(false);
        transform.position = startingPos;
        gameObject.SetActive(true);
        SetUp();

    }

    private void SetUp()
    {
        collider.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.rotation = Quaternion.identity;
        IsBeingHeld = false;

    }

    #endregion

    #region collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Respawn();
        }
    }

    #endregion
}

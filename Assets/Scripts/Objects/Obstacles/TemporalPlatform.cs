using UnityEngine;

public class TemporalPlatform : MonoBehaviour
{
    [Header("References")]
    private Animator animator;

    [Header("Settings")]
    [SerializeField] private float upTime;

    private float time = 0f;
    private bool stepped = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (stepped)
        {
            time += Time.deltaTime;

            if (time >= upTime)
            {
                stepped = false;
                time = 0f;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!stepped && collision.gameObject.CompareTag("Player"))
        {
            stepped = true;
            animator.Play("blink");
        }
    }
}

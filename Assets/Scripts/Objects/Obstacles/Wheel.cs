using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : PuzzleReceiver
{
    [Header("References")]
    [SerializeField] private List<GameObject> defaultPoints;
    [SerializeField] private List<GameObject> usedPoints;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool wait;
    [SerializeField] private float waitTime;

    private GameObject nextPoint;

    private int currentPoint = 0;
    private bool waiting = false;
    private float waitingTime = 0f;
    private bool used = false;
    private bool lost = false;

    private List<GameObject> Points
    {
        get
        {
            return used ? usedPoints : defaultPoints;
        }
    }

    private void Awake()
    {
        nextPoint = Points[currentPoint];
    }

    private void Update()
    {
        if (wait && waiting)
        {
            waitingTime += Time.deltaTime;

            if (waitingTime > waitTime)
            {
                waiting = false;
                waitingTime = 0f;
            }

            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPoint.transform.position, Time.deltaTime * moveSpeed);

        if (transform.position == nextPoint.transform.position)
        {
            nextPoint = Points[++currentPoint % (Points.Count)];
            waiting = true;
        }
    }
    public override void Actuate()
    {
        used = true;
        nextPoint = Points[currentPoint % (Points.Count)];
    }

    public override void Restore()
    {
        used = false;
        nextPoint = Points[currentPoint % (Points.Count)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerHurtbox.TouchingWheel == gameObject)
        {
            GameController.Instance.LoseLevel();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerHurtbox.TouchingWheel == gameObject && !lost)
        {
            lost = true;
            GameController.Instance.LoseLevel();
        }
    }


}

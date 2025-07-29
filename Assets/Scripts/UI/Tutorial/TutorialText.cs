using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI tutorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && tutorialText) tutorialText.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && tutorialText) tutorialText.enabled = false;
    }
}

using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    private static readonly WarningManager instance;

    public static WarningManager Instance
    {
        get
        {
            return instance != null ? instance : FindAnyObjectByType<WarningManager>();
        }
    }

    [Header("References")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI warningText;

    public void ShowWarning(string warning, float time)
    {
        StartCoroutine(ShowTimedWarning(warning, time));
    }

    private IEnumerator ShowTimedWarning(string warning, float time)
    {
        background.enabled = true;
        warningText.enabled = true;
        warningText.text = warning;

        yield return new WaitForSeconds(time);

        background.enabled = false;
        warningText.enabled = false;
    }
}

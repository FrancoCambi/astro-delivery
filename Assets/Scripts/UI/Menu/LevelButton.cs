using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int level;

    private Button btn;
    private Vector3 upScale = new Vector3(1.2f, 1.2f, 1);

    private void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        StartCoroutine(WaitForAnim());
    }
    private void Anim()
    {
        LeanTween.scale(gameObject, upScale, 0.1f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setDelay(0.1f);
    }

    private IEnumerator WaitForAnim()
    {
        Anim();
        yield return new WaitForSeconds(0.15f);
        LevelManager.Instance.LoadLevel(level);
    }
}

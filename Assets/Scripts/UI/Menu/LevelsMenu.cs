using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mainGO;
    [SerializeField] private GameObject levelsGO;
    [SerializeField] private RectTransform levelPagesRect;
    [SerializeField] private Image[] barImages;
    [SerializeField] private Button previousBtn, nextBtn;
    [SerializeField] private LevelPage[] pages;

    [Header("Settings")]
    [SerializeField] private int maxPage;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;

    private int currentPage;
    private Vector3 targetPos;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        UpdateBar();
        UpdateArrows();
    }

    private void Start()
    {
        pages[0].MakeInteractable();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            pages[currentPage - 1].MakeNotInteractable();
            currentPage++;
            pages[currentPage - 1].MakeInteractable();
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            pages[currentPage - 1].MakeNotInteractable();
            currentPage--;
            pages[currentPage - 1].MakeInteractable();
            targetPos -= pageStep;
            MovePage();
        }
    }

    public void Back()
    {
        levelsGO.SetActive(false);

        mainGO.SetActive(true);
    }

    public void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrows();
    }

    private void UpdateBar()
    {
        foreach (Image image in barImages)
        {
            image.color = Color.white;
        }
        barImages[currentPage - 1].color = Color.magenta;
    }

    private void UpdateArrows()
    {
        nextBtn.interactable = true;
        previousBtn.interactable = true;
        if (currentPage == 1) previousBtn.interactable = false;
        else if (currentPage == maxPage) nextBtn.interactable = false;
    }

}

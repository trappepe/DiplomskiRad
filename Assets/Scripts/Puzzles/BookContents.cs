using UnityEngine;
using TMPro;

public class BookContents : MonoBehaviour
{
    [TextArea(10, 20)]
    [SerializeField] private string content;

    [Header("Pages")]
    [SerializeField] private TMP_Text leftSide;
    [SerializeField] private TMP_Text rightSide;

    [Header("Page Numbers")]
    [SerializeField] private TMP_Text leftPageNumber;
    [SerializeField] private TMP_Text rightPageNumber;
    private int leftPage = 1;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        leftSide.text = content;
        rightSide.text = content;

        leftSide.ForceMeshUpdate();
        rightSide.ForceMeshUpdate();

        leftPage = 1;

        ApplyPages();
    }

    private void ApplyPages()
    {
        leftSide.pageToDisplay = leftPage;
        rightSide.pageToDisplay = leftPage + 1;

        leftSide.ForceMeshUpdate();
        rightSide.ForceMeshUpdate();

        UpdateUI();
    }

    private void UpdateUI()
    {
        leftPageNumber.text = leftPage.ToString();
        rightPageNumber.text = (leftPage + 1).ToString();
    }

    public void NextPage()
    {
        AudioController.instance.BookFlip();
        leftSide.ForceMeshUpdate();

        int maxPages = leftSide.textInfo.pageCount;

        if (leftPage + 1 >= maxPages)
            return;

        leftPage += 2;

        ApplyPages();
    }

    public void PreviousPage()
    {
        AudioController.instance.BookFlip();
        if (leftPage <= 1)
            return;

        leftPage -= 2;

        ApplyPages();
    }

}

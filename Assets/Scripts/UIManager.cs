using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Info Panel")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text infoText;
    [Header("DialogueInteraction")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [Header("Choices")]
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private GameObject puzzlePanel;
    void Awake()
    {
        instance = this;
    }

    public void SetObjects(GameObject info, TMP_Text text, GameObject dialogue, TMP_Text dialogueText, Button yesButton, Button noButton, GameObject puzzle)
    {
        this.infoPanel = info;
        this.infoText = text;
        this.dialoguePanel = dialogue;
        this.dialogueText = dialogueText;
        this.yesButton = yesButton;
        this.noButton = noButton;
        this.puzzlePanel = puzzle;
    }

    public void ShowInfo(string message)
    {
        infoText.text = message;
        infoPanel.SetActive(true);
    }
    public void HideInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }
    public void ShowDialogue(string message)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(true);
        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(HideDialogue);
        noButton.onClick.AddListener(GameManager.instance.UnlockMovementAndCamera);
    }
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowChoice(string message, Action onYes)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        yesButton.onClick.AddListener(() =>
        {
            HideDialogue();
            onYes?.Invoke();
        });
        noButton.onClick.AddListener(HideDialogue);
        noButton.onClick.AddListener(GameManager.instance.UnlockMovementAndCamera);
    }

    public void ShowPanel()
    {
        puzzlePanel.SetActive(true);
    }

    public void HidePanel()
    {
        puzzlePanel.SetActive(false);
    }
}
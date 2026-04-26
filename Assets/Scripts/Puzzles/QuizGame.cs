using UnityEngine;
using System.IO;
using UnityEngine.UI; //for elements like Buttons and Text
using System.Linq; //required for List operations
using TMPro;
using Assets.Scripts.Core;

public class QuizGame : MonoBehaviour
{
    public QuizData myQuizData;
    private string fileName = "kviz.json";
    public Text questionDisplay;
    public Button[] answerButtons;
    private int currentIdx = 0;
    private int numberOfCorrect = 0;
    private PlayerController playerMovement;
    private PlayerCamera cameraScript;
    public Button finishButton;
    [SerializeField] private int correctToFinish = 5;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerController>();
        cameraScript = FindFirstObjectByType<PlayerCamera>();

        currentIdx = 0;
        numberOfCorrect = 0;
        finishButton.gameObject.SetActive(false);
        LoadQuiz();
        if (myQuizData != null && myQuizData.kviz.Length > 0)
        {
            ShuffleQuestions();
            DisplayQuestion(0);
        }
    }

    void LoadQuiz()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            myQuizData = JsonUtility.FromJson<QuizData>(jsonString);
            Debug.Log("Kviz uspešno učitan!");
        }
        else
        {
            Debug.LogError("Datoteka nije pronađena na: " + filePath);
        }
    }
    void DisplayQuestion(int index)
    {
        QuestionsData q = myQuizData.kviz[index];
        questionDisplay.text = q.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = true;



            TMP_Text txt = answerButtons[i].GetComponentInChildren<TMP_Text>();
            txt.text = q.choices[i];

            
             int captureIndex = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(captureIndex));
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    void CheckAnswer(int selectedIdx)
    {
        int correctIdx = myQuizData.kviz[currentIdx].answerIndex;

        foreach (var btn in answerButtons)
            btn.interactable = false;

        if (selectedIdx == correctIdx)
        {
            numberOfCorrect++;
            AudioController.instance.CorrectAnswer();
 
        }
        else
        {
            AudioController.instance.WrongAnswer();
        }

        if (numberOfCorrect >= correctToFinish)
    {
        Invoke(nameof(FinishEarly), 0.75f);
    }
    else
    {
        Invoke(nameof(NextQuestion), 0.75f);
    }

    }

    void NextQuestion()
    {
        currentIdx++;
        if (currentIdx < myQuizData.kviz.Length)
        {
            DisplayQuestion(currentIdx);
        }
    }
    void ShuffleQuestions()
    {
        for (int i = 0; i < myQuizData.kviz.Length; i++)
        {
            QuestionsData temp = myQuizData.kviz[i];
            int randomIndex = Random.Range(i, myQuizData.kviz.Length);
            myQuizData.kviz[i] = myQuizData.kviz[randomIndex];
            myQuizData.kviz[randomIndex] = temp;
        }
    }
    void FinishEarly()
    {
        questionDisplay.text = $"Bravo! Tačno si odgovorio na {numberOfCorrect} pitanja!";

        finishButton.gameObject.SetActive(true);
        GameManager.instance.GiveItem(ItemTypes.Drink);
        Debug.Log("Drink aquired");


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
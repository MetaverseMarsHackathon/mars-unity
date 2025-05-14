using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuizUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelDescription;
    public GameObject panelQuestion;
    
    [Header("Texts")]
    public TMP_Text descriptionText;
    public TMP_Text questionText;
    public TMP_Text feedbackText;
    
    [Header("Buttons")]
    public Button buttonO;
    public Button buttonX;
    public Button nextButton;

    private string correctAnswer;
    private Action onCorrectAnswer;  // ✅ 콜백 저장

    public void SetQuiz(QuizData data, Action onCorrect = null)
    {
        descriptionText.text = data.description;
        questionText.text = data.question;
        correctAnswer = data.answer;
        onCorrectAnswer = onCorrect;  // ✅ 외부에서 전달된 액션 저장

        panelDescription.SetActive(true);
        panelQuestion.SetActive(false);
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(false);

        buttonO.gameObject.SetActive(true);
        buttonX.gameObject.SetActive(true);
    }

    void Start()
    {
        nextButton.onClick.AddListener(ShowQuestionPanel);
        buttonO.onClick.AddListener(() => CheckAnswer("O"));
        buttonX.onClick.AddListener(() => CheckAnswer("X"));
    }
    
    void ShowQuestionPanel()
    {
        panelDescription.SetActive(false);
        panelQuestion.SetActive(true);
        feedbackText.gameObject.SetActive(false);
    }

    void CheckAnswer(string selected)
    {
        bool isCorrect = selected == correctAnswer;

        feedbackText.text = isCorrect ? "정답입니다!" : "오답입니다.";
        feedbackText.color = isCorrect ? Color.green : Color.red;
        feedbackText.gameObject.SetActive(true);

        buttonO.gameObject.SetActive(false);
        buttonX.gameObject.SetActive(false);

        if (isCorrect)
        {
            onCorrectAnswer?.Invoke();  // ✅ 정답이면 액션 실행
        }
    }
}
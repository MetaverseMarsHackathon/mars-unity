using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelDescription;
    public GameObject panelQuestion;
    
    [Header("Texts")]
    public Text descriptionText;
    public Text questionText;
    public Text feedbackText;
    
    [Header("Buttons")]
    public Button buttonO;
    public Button buttonX;
    public Button nextButton;

    private string correctAnswer;

    public void SetQuiz(QuizData data)
    {
        descriptionText.text = data.description;
        questionText.text = data.question;
        correctAnswer = data.answer;
        
        panelDescription.SetActive(true);
        panelQuestion.SetActive(false);
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(false);
    }

    void Start()
    {
        nextButton.onClick.AddListener(ShowQuestionPanel);
        buttonO.onClick.AddListener(() => CheckAnswer("O"));
        buttonX.onClick.AddListener(() => CheckAnswer("X"));
    }
    
    void ShowQuestionPanel()
    {
        // 설명 패널 숨기고 문제 패널 보이기
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

        // 정답 선택 후 선택 버튼 비활성화
        buttonO.gameObject.SetActive(false);
        buttonX.gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Text descriptionText;
    public Text questionText;
    public Button buttonO;
    public Button buttonX;
    public Text feedbackText;

    private string correctAnswer;

    public void SetQuiz(QuizData data)
    {
        descriptionText.text = data.description;
        questionText.text = data.question;
        correctAnswer = data.answer;
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(false);

        buttonO.gameObject.SetActive(true);
        buttonX.gameObject.SetActive(true);
    }

    void Start()
    {
        buttonO.onClick.AddListener(() => CheckAnswer("O"));
        buttonX.onClick.AddListener(() => CheckAnswer("X"));
    }

    void CheckAnswer(string selected)
    {
        if (selected == correctAnswer)
        {
            Debug.Log("Correct");
            feedbackText.text = "정답입니다!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "오답입니다.";
            feedbackText.color = Color.red;
        }
        
        feedbackText.gameObject.SetActive(true);
        buttonO.gameObject.SetActive(false);
        buttonX.gameObject.SetActive(false);
    }
}

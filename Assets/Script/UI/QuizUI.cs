using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Collections;
using System.Text; 

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
    
    [Header("Quiz Data")]
    private string correctAnswer;
    public int currentQuestionIndex;
    
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

        currentQuestionIndex = gameObject.GetComponent<QuizTrigger>().currentQuestionIndex; 
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
        
        StartCoroutine(SendAnswer(currentQuestionIndex, questionText.text, selected, isCorrect));
    }
    
    [System.Serializable]
    public class AnswerData
    {
        public int questionNumber;
        public string questionText;
        public string userAnswer;
        public bool correct;
    }
    
    IEnumerator SendAnswer(int questionNumber, string questionText, string userAnswer, bool correct)
    {
        string sessionId = (LoginManager.SessionId).ToString();
        string url = $"http://172.16.16.170:8081/question/{sessionId}/response";
        
        Debug.Log(url);

        AnswerData data = new AnswerData
        {
            questionNumber = questionNumber,
            questionText = questionText,
            userAnswer = userAnswer,
            correct = correct
        };

        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("서버 응답 성공: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }
}
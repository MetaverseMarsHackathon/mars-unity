using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Complete : MonoBehaviour
{
    private int id;
    private string scoreUrl; // 여기에 실제 서버 주소 넣기
    private string baseurl = "http://172.16.16.170:8081/game/";
    private int _time = 300; //여기 실제 타임 넘겨주면 됩니다.
    
    public void EndButton()
    {
        id = LoginManager.SessionId;
        scoreUrl =  baseurl + id + "/complete";
        StartCoroutine(SendCompletionTime(_time)); // 테스트용으로 0초 전달
        print(scoreUrl);
    }

    IEnumerator SendCompletionTime(int completionTime)
    {
        // 보낼 JSON 데이터
        var data = new
        {
            completionTime = completionTime
        };

        string jsonData = JsonConvert.SerializeObject(data);

        UnityWebRequest request = new UnityWebRequest(scoreUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("응답 수신: " + request.downloadHandler.text);

            // 응답 데이터 파싱
            ScoreResponse response = JsonConvert.DeserializeObject<ScoreResponse>(request.downloadHandler.text);
            Debug.Log($"Score: {response.score}, Correct: {response.correctAnswers}/{response.totalQuestions}, Time: {response.completionTime}");
        }
        else
        {
            Debug.LogError("요청 실패: " + request.error);
        }
    }

    // 응답 JSON 구조
    class ScoreResponse
    {
        public int score;
        public int totalQuestions;
        public int correctAnswers;
        public int completionTime;
    }
}
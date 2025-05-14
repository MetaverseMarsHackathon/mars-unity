using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Review : MonoBehaviour
{
    private static int id = LoginManager.SessionId;
    string reviewUrl = "http://172.16.16.170:8081/api/game/"+id+"/review"; // 실제 GET API 주소로 교체

    void ReviewStart() // 리뷰가 필요할 시점에 이거 불러주시면 됩니다. 
    {
        StartCoroutine(GetReview());
    }

    IEnumerator GetReview()
    {
        UnityWebRequest request = UnityWebRequest.Get(reviewUrl);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("GET 응답 수신: " + request.downloadHandler.text);

            // JSON 응답 파싱
            ReviewResponse response = JsonConvert.DeserializeObject<ReviewResponse>(request.downloadHandler.text);

            Debug.Log("틀린 문제 리스트:");
            foreach (string question in response.incorrectQuestions)
            {
                Debug.Log("- " + question);
            }

            Debug.Log("해설 내용: " + response.reviewContent.explanations);
        }
        else
        {
            Debug.LogError("GET 요청 실패: " + request.error);
        }
    }

    // 응답 구조 정의
    class ReviewResponse
    {
        public List<string> incorrectQuestions;
        public ReviewContent reviewContent;
    }

    class ReviewContent
    {
        public string explanations;
    }
}
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance { get; private set; }

    public static int SessionId; // 전역 접근 가능한 static 변수

    string loginUrl = "http://172.16.16.170:8081/auth/login";

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴 방지
    }

    public void LoginStart()
    {
        StartCoroutine(Login("test", "test"));
        Debug.Log("버튼 클릭됨");
    }

    IEnumerator Login(string username, string password)
    {
        var loginData = new
        {
            username = username,
            password = password
        };

        string jsonData = JsonConvert.SerializeObject(loginData);

        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var response = JsonConvert.DeserializeObject<LoginResponse>(request.downloadHandler.text);
            SessionId = response.sessionId;
            Debug.Log($"[Login Success] Session ID: {SessionId}");
        }
        else
        {
            Debug.LogError("Login Failed: " + request.error);
        }
    }

    class LoginResponse
    {
        public int sessionId;
        public string username;
        public bool success;
        public string message;
    }
}
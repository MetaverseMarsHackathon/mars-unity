using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizUIPrefab;
    public QuizData quizData; // 에디터에서 설정 또는 런타임에 불러오기

    private GameObject currentUI;
    
    // UI가 플레이어 앞 몇 미터에 생성될지 설정
    public float spawnDistanceFromCamera = 2.5f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter {other.name}");
        if (other.CompareTag("Player") && currentUI == null)
        {
            Debug.Log($"OnTriggerEnter {other.name}");
            // 카메라 Transform 얻기
            Transform cameraTransform = Camera.main.transform;

            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * spawnDistanceFromCamera;
            float minY = 1.2f;
            // Y값 보정 
            spawnPosition.y = Mathf.Max(spawnPosition.y, minY);

            currentUI = Instantiate(quizUIPrefab, spawnPosition, Quaternion.identity);
            currentUI.SetActive(true); 
            currentUI.transform.LookAt(cameraTransform);
            Vector3 direction = cameraTransform.position - transform.position;
            direction.y = 0; 
            currentUI.transform.rotation = Quaternion.LookRotation(-direction);
            
            //currentUI.transform.rotation = Quaternion.LookRotation(currentUI.transform.position - cameraTransform.position); // UI가 카메라를 바라보도록

            var quizUI = currentUI.GetComponent<QuizUI>();
            quizUI.SetQuiz(quizData);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentUI != null)
        {
            Destroy(currentUI);
        }
    }
}
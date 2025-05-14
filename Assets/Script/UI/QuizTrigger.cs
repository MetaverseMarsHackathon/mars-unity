using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizUIPrefab;
    public QuizData quizData;

    public GameObject fixObject;  // ✅ 정답 후 보여줄 오브젝트 또는 실행할 행동

    private GameObject currentUI;
    public float spawnDistanceFromCamera = 2.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentUI == null)
        {
            Transform cameraTransform = Camera.main.transform;

            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * spawnDistanceFromCamera;
            spawnPosition.y = cameraTransform.position.y - 0.03f;

            currentUI = Instantiate(quizUIPrefab, spawnPosition, Quaternion.identity);
            currentUI.SetActive(true);

            // UI가 플레이어를 바라보게 회전
            Vector3 direction = cameraTransform.position - transform.position;
            direction.y = 0;
            currentUI.transform.rotation = Quaternion.LookRotation(-direction);

            // ✅ 퀴즈 UI에 콜백 포함해서 전달
            var quizUI = currentUI.GetComponent<QuizUI>();
            quizUI.SetQuiz(quizData, () => {
                Debug.Log("정답! 지정된 오브젝트 또는 애니메이션 실행");
                if (fixObject != null)
                    fixObject.SetActive(true); // 애니메이션 트리거 or 오브젝트 활성화
            });
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
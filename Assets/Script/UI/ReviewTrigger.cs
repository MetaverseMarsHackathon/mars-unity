using System.Collections;
using UnityEngine;

public class ReviewTrigger : MonoBehaviour
{
    public GameObject reviewUIPrefab;
    private GameObject currentUI;

    public Review review;
    public GameObject objectToActivate; // ⭐

    public string reviewText = "# 화성 탐사 복습 노트";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentUI == null)
        {
            StartCoroutine(WaitProcess());
        }
    }

    private IEnumerator WaitProcess()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 3.5f;
        spawnPosition.y = cameraTransform.position.y - 0.05f;

        currentUI = Instantiate(reviewUIPrefab, spawnPosition, Quaternion.identity);

        Vector3 lookDirection = cameraTransform.position - currentUI.transform.position;
        lookDirection.y = 0;
        currentUI.transform.rotation = Quaternion.LookRotation(-lookDirection);

        review.ReviewStart();

        yield return new WaitWhile(() => string.IsNullOrEmpty(Review.result));

        ReviewUI reviewUI = currentUI.GetComponent<ReviewUI>();
        if (reviewUI != null)
        {
            reviewUI.SetDescription(Review.result);

            // ⭐ 버튼 누르면 오브젝트 활성화
            reviewUI.SetEndCallback(() => {
                if (objectToActivate != null)
                    objectToActivate.SetActive(true);
            });
        }
    }
}



using UnityEngine;

public class ReviewTrigger : MonoBehaviour
{
    public GameObject reviewUIPrefab;
    private GameObject currentUI;

    public Review review;
    //public string reviewText = "자동 생성되는 복습 텍스트입니다.";

    public string reviewText =
        "# 화성 탐사 복습 노트"; 
    

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌했음");
        if (other.CompareTag("Player") && currentUI == null)
        {
            Debug.Log("플레이어와 충돌했음");
            currentUI = Instantiate(reviewUIPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);

            //ReviewUI reviewUI = currentUI.GetComponent<ReviewUI>();
            //reviewUI.SetDescription(reviewText);
            

            Invoke("Time",5f);
            review.ReviewStart();
            Debug.Log(Review.result);
        }
    }

    void Time()
    {
        ReviewUI reviewUI = currentUI.GetComponent<ReviewUI>();
        reviewUI.SetDescription(Review.result);
    }
}

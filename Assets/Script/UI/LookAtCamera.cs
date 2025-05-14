using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform cameraTransform;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        transform.LookAt(cameraTransform);
        
        //Vector3 direction = cameraTransform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(direction);
        
        //Vector3 direction = cameraTransform.position - transform.position;
        //direction.y = 0; // 위아래 회전 고정하고 싶으면 이 줄 유지
        //transform.rotation = Quaternion.LookRotation(-direction);
    }
}

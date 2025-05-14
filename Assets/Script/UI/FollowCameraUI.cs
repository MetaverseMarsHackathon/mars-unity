using UnityEngine;

public class FollowCameraUI : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceFromCamera = 2f;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        transform.position = cameraTransform.position + forward * distanceFromCamera;
        transform.rotation = Quaternion.LookRotation(forward);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAction : MonoBehaviour
{
    [Header("🎮 Input Actions")]
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private InputActionProperty moveInput;

    [Header("📦 Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform xrCamera; // ✅ 카메라 참조 추가

    [Header("⚙️ Settings")]
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float marsGravity = 0.38f;

    private float gravity;
    private float verticalVelocity;
    private bool isGrounded;

    private void Start()
    {
        gravity = Physics.gravity.y * marsGravity;
    }

    private void Update()
    {
        isGrounded = IsGrounded();

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -1f;
        }

        if (jumpButton.action.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        // ✅ 카메라 기준 이동 방향
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 inputDirection = new Vector3(input.x, 0, input.y);

        Vector3 camForward = xrCamera.forward;
        Vector3 camRight = xrCamera.right;

        // 수평 방향만 사용
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camRight * input.x + camForward * input.y) * moveSpeed;

        // 중력 적용
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        Vector3 pos = transform.position + Vector3.down * 0.1f;
        return Physics.CheckSphere(pos, 0.3f, groundLayer);
    }
}
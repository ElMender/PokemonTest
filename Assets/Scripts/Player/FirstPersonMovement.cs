using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Valores de control")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Camara")]
    [SerializeField] private CinemachineCamera virtualCamera;

    [SerializeField] AudioSource stepSFX;

    private CharacterController controller;
    private Transform cameraHolder;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraHolder = virtualCamera.transform;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        if (controller.velocity.magnitude > 0)
        {
            stepSFX.enabled = true;
        }
        else
        {
            stepSFX.enabled = false;
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime); 
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Camera tilt
        transform.Rotate(Vector3.up * mouseX); // Player body rotation
    }
}
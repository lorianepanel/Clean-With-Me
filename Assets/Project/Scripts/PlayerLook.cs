using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 50f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float maxYRotation = 90f;
    [SerializeField] private float minYRotation = -90f;

    private float _currentYRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleRotationInput();
    }

    private void HandleRotationInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        _currentYRotation += mouseX;
        _currentYRotation = Mathf.Clamp(_currentYRotation, minYRotation, maxYRotation);

        Quaternion targetRotation = Quaternion.Euler(0f, _currentYRotation, 0f);
        playerBody.rotation = targetRotation;

        // Resetting the local rotation of the camera so it doesn't rotate on X-axis
        transform.localRotation = Quaternion.identity;
    }
}

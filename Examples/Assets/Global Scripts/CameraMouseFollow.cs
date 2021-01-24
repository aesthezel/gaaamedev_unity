using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMouseFollow : MonoBehaviour
{
    private const float ROTATE_LIMIT = 90f;
    private const string    INPUT_MOUSE_X = "Mouse X", 
                            INPUT_MOUSE_Y = "Mouse Y";

    [SerializeField] private float mouseSensivity = 50f;
    [SerializeField] private Transform targetToRotate;

    private float rotationX = Vector3.zero.x;

    #region Class Logic
    private float InputMouse(float input) => input * mouseSensivity * Time.deltaTime;

    private void RotateCameraOnX()
    {
        rotationX -= InputMouse(Input.GetAxis(INPUT_MOUSE_Y));
        rotationX = Mathf.Clamp(rotationX, -ROTATE_LIMIT, ROTATE_LIMIT);
        transform.localRotation = Quaternion.Euler(rotationX, Vector3.zero.y, Vector3.zero.z);
    }

    private void RotateTargetLateral() => targetToRotate.Rotate(Vector3.up * InputMouse(Input.GetAxis(INPUT_MOUSE_X)));
    #endregion

    #region MonoBehaviour API
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCameraOnX();
        RotateTargetLateral();
    }
    #endregion
}

using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
     public float mouseSensitivity = 3.0f;
    public Transform target;
    public float distanceFromTarget = 3.0f;
    public float smoothTime = 0.2f;
    private float rotationY;
    private float rotationX;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -40f, 40f);
        Vector3 nextRotation = new Vector3(rotationX, rotationY, 0);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distanceFromTarget;
    }

    public void ResetRotations()
    {
        rotationX = 0;
        rotationY = 0;
        currentRotation = Vector3.zero;
    }
}

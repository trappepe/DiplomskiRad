using System.Collections;
using UnityEngine;

public class FocusCamera: MonoBehaviour
{
    public float smoothTime = 1.5f;

    private Transform mainCam;
    private Vector3 originalPos;
    private Quaternion originalRot;

    void Start()
    {
        GameManager.instance.focusCamera = this;
    }

    public void StartFocus(Vector3 targetPosition, Vector3 targetRotation)
    {
        mainCam = Camera.main.transform;
        originalPos = mainCam.position;
        originalRot = mainCam.rotation;

        StopAllCoroutines();
        StartCoroutine(FocusRoutine(targetPosition, targetRotation));
    }

    IEnumerator FocusRoutine(Vector3 targetPosition, Vector3 targetRotation)
    {
        GameManager.instance.LockMovementAndCamera();

        Vector3 startPos = mainCam.position;
        Quaternion startRot = mainCam.rotation;

        Quaternion targetQuat = Quaternion.Euler(targetRotation);

        float t = 0;

        while (t < smoothTime)
        {
            t += Time.deltaTime;
            float s = Mathf.SmoothStep(0, 1, t / smoothTime);

            mainCam.position = Vector3.Lerp(startPos, targetPosition, s);
            mainCam.rotation = Quaternion.Slerp(startRot, targetQuat, s);

            yield return null;
        }
    }

    public void StopFocus()
    {
        StartCoroutine(ReturnCamera());
    }

    IEnumerator ReturnCamera()
    {
        Vector3 currentPos = mainCam.position;
        Quaternion currentRot = mainCam.rotation;

        float elapsed = 0;

        while (elapsed < smoothTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / smoothTime);

            mainCam.position = Vector3.Lerp(currentPos, originalPos, t);
            mainCam.rotation = Quaternion.Slerp(currentRot, originalRot, t);

            yield return null;
        }

        GameManager.instance.UnlockMovementAndCamera();
    }
}
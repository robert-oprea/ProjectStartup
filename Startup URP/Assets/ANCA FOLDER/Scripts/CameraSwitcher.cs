using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera otherCamera;
    public float switchDuration = 1.0f;

    private Camera _activeCamera;

    public Camera ActiveCamera
    {
        get { return _activeCamera; }
    }

    void Start()
    {
        _activeCamera = mainCamera;
        SwitchCamerasInstant();
    }

    void Update()
    {
        // Example: Switch cameras when the Spacebar is pressed
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SwitchCamerasSmoothCoroutine());
        }*/
    }

    void SwitchCamerasInstant()
    {
        mainCamera.enabled = (_activeCamera == mainCamera);
        otherCamera.enabled = (_activeCamera == otherCamera);
    }

    public IEnumerator SwitchCamerasSmoothCoroutine()
    {
        mainCamera.enabled = false;
        otherCamera.enabled = true;

        float elapsedTime = 0f;

        while (elapsedTime < switchDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.enabled = false;
        otherCamera.enabled = true;

        _activeCamera = otherCamera;
    }


    public IEnumerator SwitchBackToMainCamera()
    {
        mainCamera.enabled = true;
        otherCamera.enabled = false;

        float elapsedTime = 0f;

        while (elapsedTime < switchDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.enabled = true;
        otherCamera.enabled = false;

        _activeCamera = mainCamera;
    }
}

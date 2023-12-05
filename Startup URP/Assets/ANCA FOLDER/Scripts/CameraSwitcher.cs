using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera; //the main camera
    public Camera otherCamera; //the dialogue camera
    public float switchDuration = 1.0f; 

    private Camera _activeCamera;

    //returning current active camera
    public Camera ActiveCamera
    {
        get { return _activeCamera; }
    }

    void Start()
    {
        _activeCamera = mainCamera; //main camera is our default active camera
        SwitchCamerasInstant();
    }

    //bools for detecting which camera should be enabled or disabled
    void SwitchCamerasInstant()
    {
        mainCamera.enabled = (_activeCamera == mainCamera);
        otherCamera.enabled = (_activeCamera == otherCamera);
    }

    //switching to dialogue camera 
    public IEnumerator SwitchToDialogueCamera()
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

    //switching back to main camera 
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

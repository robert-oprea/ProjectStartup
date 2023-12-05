using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //sets target which will be the player

    public float smoothSpeed = 8f; //the speed the camera has when moving after target

    public Vector3 offset;

    void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z); //the position we want our camera to be at based on player position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); //transforming the camera's position after player's position with a smoothing effect
        transform.position = smoothedPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Click : MonoBehaviour
{
    FadeAndDestroy fadeDestroy;

    private void Start()
    {
        fadeDestroy = GetComponent<FadeAndDestroy>();
    }

    Vector3 MouseWorldPosition()
    {

        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }


    public void OnMouseDown()
    {

        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "IMPOSTOR")
            {
                Debug.Log("clicked on impostor");
                fadeDestroy.StartCoroutine(fadeDestroy.FadeTo(fadeDestroy.alphaValue, fadeDestroy.fadeDelay));


            }
            else
            {
                Debug.Log("clicked smth");

            }


        }


    }

}

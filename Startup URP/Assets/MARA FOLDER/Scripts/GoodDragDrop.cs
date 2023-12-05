using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodDragDrop : MonoBehaviour
{
    Vector3 offset;

    Vector3 startingPos;


    [SerializeField]
    WordManager wordManager;

    FadeAndDestroy fadeDestroy;


    private void Start()
    {

        startingPos = this.transform.position;
        fadeDestroy = GetComponent<FadeAndDestroy>();

    }


    Vector3 MouseWorldPosition()
    {

        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }




    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    //string translation;

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo))
        {

            //check if translation the same as the text in the box

            string translation = hitInfo.transform.gameObject.GetComponent<TextMeshPro>().text;

            if (wordManager.Translate(transform.gameObject.GetComponent<TextMeshPro>().text) == translation)
            {
                transform.position = hitInfo.transform.position;
                Debug.Log("right combo");
                fadeDestroy.StartCoroutine(fadeDestroy.FadeTo(fadeDestroy.alphaValue, fadeDestroy.fadeDelay, hitInfo.transform.gameObject));

            }else
            {
                transform.position = startingPos;

            }

        }else
        {
            transform.position = startingPos;
        }

        transform.GetComponent<Collider>().enabled = true;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragDrop : MonoBehaviour
{

    Vector3 offset;


    [SerializeField]
    public string destinationTag;

    [SerializeField]
    private SceneSwitch sceneSwitch = null;

    private string currentSceneName;


    Vector3 startingPos;

    private void Start()
    {
        startingPos = this.transform.position;

        currentSceneName = SceneManager.GetActiveScene().name;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }


    public void StartFading(Renderer rend1, Renderer rend2)
    {
        StartCoroutine(FadeOut( rend1, rend2));
    }


    IEnumerator FadeOut(Renderer rend1, Renderer rend2)
    {


        Debug.Log("fadeout start");
        //fade 2 things that matched together
        Color c;
        for (float f=1.0f; f>= -0.1f; f -= 0.1f)
        {
            //fade thing 1
            c = rend1.material.color;
            c.a = f;
            rend1.material.color = c;

            //fade thing 2
            c = rend2.material.color;
            c.a = f;
            rend2.material.color = c;

            yield return new WaitForSeconds(0.02f);
            Debug.Log("fading");

        }
        Debug.Log("fadeout end");

        //onFadeComplete.Invoke();
        

        sceneSwitch.LoadLevel("3 MARA SCENE");


        Debug.Log("REMOVE");
        
        //Destroy();


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

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag(destinationTag))
            {
                transform.position = hitInfo.transform.position;
                Debug.Log("right combo");

                //StartFading(this.gameObject, hitInfo.);
                StartFading(transform.GetComponent<Renderer>(), hitInfo.transform.GetComponent<Renderer>());

                // Start fading and load the new scene when the fading is complete
               // StartFading(transform.GetComponent<Renderer>(), hitInfo.transform.GetComponent<Renderer>(), LoadNewScene);

                //Renderer rendWord = transform.GetComponent<Renderer>();
                //Renderer rendImg = hitInfo.transform.GetComponent<Renderer>();

                //IDEA FOR FUTURE USE
                // Have a method to check which puzzle you solved and epending on the name of the npc or smth you get sent to a different outcome/sollution
                //like for the drag&drop tou go to fadeout and go next and for the flower you go to another emthod


                //ecel file with all the words in the first column and tags in the second and i make an algorithm that reads that file and categorizes the words
                //for example banana has yellow and fruit tag and if topic is fruit the algorithm looks in second column for fruit and when find it picks it and puts it in the empty gameobject on the petal

                //one big xl file for all the words llow ppl to make any cuts and check if the cutout is in the database

                //get ready for the limited real time event in the database add christmas tag for the words in the dictionary

            }
            else
            {
                transform.position = startingPos;
            }

        }
        else
        {
            transform.position = startingPos;
        }
        transform.GetComponent<Collider>().enabled = true;
    }

    /*void LoadNewScene()
    {
        // Assuming you have a SceneSwitch script attached to the same GameObject
        // Make sure to assign the SceneSwitch component in the Unity Editor
        if (sceneSwitch != null)
        {
            sceneSwitch.LoadLevel("3 MARA SCENE");
        }
        else
        {
            Debug.LogError("sceneSwitch is not assigned!");
        }
    }*/


}

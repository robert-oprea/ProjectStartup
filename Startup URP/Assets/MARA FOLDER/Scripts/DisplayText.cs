using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class DisplayText : MonoBehaviour
{

    WordManager wordManager;

    [SerializeField]
    TextMeshPro[] wordSlots;

    string[] pickedWords;

    string[] translatedWords;

    string pickedTopic;

    FadeAndDestroy fadeDestroy;

    int random; 

    // Start is called before the first frame update
    void Start()
    {
        wordManager = GetComponent<WordManager>();
        random = UnityEngine.Random.Range(1, 7);

        
        switch (tag)
        {
            case "DROP":
                DragDropDisplay();
                break;

            case "FLOWER":
                FlowerDisplay();
                break;
            case "FRUIT":
                FruitDragDropDisplay();
                break;
        }

        // MAKE NEW TAG FOR FRUIT ONLY DRAG DROP


    }

    private void Update()
    {
        switch (tag)
        {
            case "DROP":
                DragDropRefresh();
                break;

            case "FLOWER":
                FlowerRefresh();
                break;
            case "FRUIT":
                FruitDragDropRefresh();
                break;
        }
    }

    void DragDropDisplay()
    {
        pickedWords = wordManager.PickWords(4);

        translatedWords = new string[pickedWords.Length];

        for (int i = 0; i < pickedWords.Length; i++)
        {
            translatedWords[i] = wordManager.Translate(pickedWords[i]);
        }

        translatedWords = wordManager.RandomizeArray(translatedWords);

        /*for (int i = 0; i < translatedWords.Length; i++)
        {
            Debug.Log(translatedWords[i]);
        }*/

        int s = 0;
        int e = 0;

        for (int i = 0; i < wordSlots.Length; i++)
        {

            switch (wordSlots[i].text)
            {
                case "english":
                    wordSlots[i].text = translatedWords[e];
                    e++;
                    break;

                case "spanish":
                    wordSlots[i].text = pickedWords[s];
                    s++;
                    break;

            }

        }

    }

    void FruitDragDropDisplay()
    {
        string[] topicWords = wordManager.PickedTopicWords("fruits");

        pickedWords = new string[4];

        for (var i = 0; i < pickedWords.Length; i++)
        {
            pickedWords[i] = topicWords[i];
        }

        translatedWords = new string[pickedWords.Length];

        for (int i = 0; i < pickedWords.Length; i++)
        {
            translatedWords[i] = wordManager.Translate(pickedWords[i]);
        }

        translatedWords = wordManager.RandomizeArray(translatedWords);

        /*for (int i = 0; i < translatedWords.Length; i++)
        {
            Debug.Log(translatedWords[i]);
        }*/

        int s = 0;
        int e = 0;

        for (int i = 0; i < wordSlots.Length; i++)
        {

            switch (wordSlots[i].text)
            {
                case "english":
                    wordSlots[i].text = translatedWords[e];
                    e++;
                    break;

                case "spanish":
                    wordSlots[i].text = pickedWords[s];
                    s++;
                    break;

            }

        }
    }

    void FruitDragDropRefresh()
    {
        bool didAllFade = true;

        for (int i = 0; i < 4; i++)
        {
            fadeDestroy = wordSlots[i].gameObject.GetComponent<FadeAndDestroy>();
            if (fadeDestroy.faded == false)
            {
                didAllFade = false;
                break;
            }
        }
        Debug.Log("didallfade  " + didAllFade);

        if (didAllFade == true)
        {
            ReloadCurrentScene();
        }
    }

    void DragDropRefresh()
    {
        //check if all the elements of array are empty
        bool didAllFade = true;

        for (int i = 0; i < 4; i++)
        {
            fadeDestroy = wordSlots[i].gameObject.GetComponent<FadeAndDestroy>();
            if (fadeDestroy.faded == false)
            {
                didAllFade = false;
                break;
            }
        }
        Debug.Log("didallfade  " + didAllFade);

        if (didAllFade == true)
        {
            ReloadCurrentScene();
        }
    }



    public void FlowerDisplay()
    {
        pickedTopic = wordManager.dictionary.allWords[UnityEngine.Random.Range(0, wordManager.dictionary.lineNr)][2];

        string[] pickedWords = wordManager.PickedTopicWords(pickedTopic);

        //pick a random one of the text thingies and write impostor in that then fill them out using what i used for the other display and check for impostor, spanish and topic

        wordSlots[random].text = "impostor";

        int s = 0;

        for (int i = 0; i < wordSlots.Length; i++)
        {
            switch (wordSlots[i].text)
            {
                case "topic":
                    wordSlots[i].text = pickedTopic;
                    break;

                case "spanish":
                    wordSlots[i].text = pickedWords[s];
                    s++;
                    break;

                case "impostor":
                    while (true)
                    {
                        int random2 = UnityEngine.Random.Range(0, wordManager.dictionary.lineNr);
                        string impostor = wordManager.dictionary.allWords[random2][0];
                        string impostorTopic = wordManager.dictionary.allWords[random2][2];
                        //Debug.Log("imostor is  " + impostor + "  and it s topic is  " + impostorTopic);
                        if (impostorTopic != pickedTopic)
                        {
                            wordSlots[i].text = impostor + " THIS";
                            wordSlots[i].tag = "IMPOSTOR";
                            break;
                        }
                    }
                    break;
            }

        }

    }


    void FlowerRefresh()
    {
        fadeDestroy = wordSlots[random].gameObject.GetComponent<FadeAndDestroy>();
        if (fadeDestroy.faded)
        {
            //GoToMainScene();

            if(SceneManager.GetActiveScene().name == "Flower SCENE 5")
            {
                GoToMainScene();
            }
            else
            {
                GoToNextScene();
            }


            /*switch (SceneManager.GetActiveScene().name)
            {
                case "Flower SCENE 1":
                case "Flower SCENE 2":
                case "Flower SCENE 3":
                case "Flower SCENE 4":
                    GoToNextScene();
                    break;
            }*/

        }
    }

    

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);

        Debug.Log("reloaded scene " + currentSceneName);

    }

    public void GoToNextScene()
    {

        Debug.Log("load next scene :   " + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("NEW");
    }

    public void GoToThisScene( string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




}

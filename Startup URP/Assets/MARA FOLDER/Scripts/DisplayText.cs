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

    private SceneSwitch sceneSwitch;

    // Start is called before the first frame update
    void Start()
    {
        wordManager = GetComponent<WordManager>();

        sceneSwitch = GetComponent<SceneSwitch>();

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
        pickedWords = wordManager.PickWords(4); // picks 4 words

        translatedWords = new string[pickedWords.Length];

        for (int i = 0; i < pickedWords.Length; i++)
        {
            // and translates each of them
            translatedWords[i] = wordManager.Translate(pickedWords[i]); 
        }

        // randomizes the translated words
        translatedWords = wordManager.RandomizeArray(translatedWords);

        int s = 0; int e = 0;

        for (int i = 0; i < wordSlots.Length; i++)
        {
            // displays the words depending on what it should be there
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

    

    public void FlowerDisplay()
    {
        // picks a random topic
        pickedTopic = wordManager.dictionary.allWords[UnityEngine.Random.Range(0, wordManager.dictionary.lineNr)][2];

        string[] pickedWords = wordManager.PickedTopicWords(pickedTopic); // and picks words with that topic

        wordSlots[random].text = "impostor"; // gets a random text gameobject and makes it the impostor

        int s = 0;

        for (int i = 0; i < wordSlots.Length; i++)
        {
            // displays the words depending on what it should be there
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
                        // picks a random word
                        string impostor = wordManager.dictionary.allWords[random2][0];
                        string impostorTopic = wordManager.dictionary.allWords[random2][2];
                        // and checks if the impostor doesn't have the chosen topic
                        if (impostorTopic != pickedTopic)
                        {
                            wordSlots[i].text = impostor;
                            wordSlots[i].tag = "IMPOSTOR";
                            break;
                        }
                    }
                    break;
            }

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
            sceneSwitch.ReloadCurrentScene();
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
            sceneSwitch.CheckIfGoodScene("Fruit SCENE 3");
        }
    }



    void FlowerRefresh()
    {
        fadeDestroy = wordSlots[random].gameObject.GetComponent<FadeAndDestroy>();
        if (fadeDestroy.faded)
        {
            sceneSwitch.CheckIfGoodScene("Flower SCENE 5");

        }
    }

    

    




}

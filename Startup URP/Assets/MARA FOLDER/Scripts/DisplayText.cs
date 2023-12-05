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

    // Start is called before the first frame update
    void Start()
    {
        wordManager = GetComponent<WordManager>();


        //Text = FindObjectOfType<TextMeshPro>();
        //Text = GetComponent<TextMeshPro>();

        /*if (tag == "DROP")
        {
            DragDropDisplay();
        }*/

        switch (tag)
        {
            case "DROP":
                DragDropDisplay();
                break;

            case "FLOWER":
                FlowerDisplay();
                break;
        }


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
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("reloaded scene " + currentSceneName);

            SceneManager.LoadScene(currentSceneName);
        }
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("NEW");
    }

    void FlowerRefresh()
    {

    }


    void FlowerDisplay()
    {
        pickedTopic = wordManager.dictionary.allWords[UnityEngine.Random.Range(0, wordManager.dictionary.lineNr)][2];

        string[] pickedWords = wordManager.PickedTopicWords(pickedTopic);

        int random = UnityEngine.Random.Range(1, 7);

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
                        random = UnityEngine.Random.Range(0, wordManager.dictionary.lineNr);
                        string impostor = wordManager.dictionary.allWords[random][0];
                        string impostorTopic = wordManager.dictionary.allWords[random][2];
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




}

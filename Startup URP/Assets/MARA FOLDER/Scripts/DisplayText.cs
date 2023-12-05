using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;
using Unity.Mathematics;

public class DisplayText : MonoBehaviour
{

    WordManager wordManager;

    [SerializeField]
    TextMeshPro[] wordSlots;

    string[] pickedWords;

    string[] translatedWords;

    string pickedTopic;


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


    void FlowerDisplay()
    {
        pickedTopic = wordManager.dictionary.allWords[UnityEngine.Random.Range(0, wordManager.dictionary.lineNr)][0];

        
    }

    void DragDropDisplay()
    {
        pickedWords = wordManager.PickWords(4);

        translatedWords = new string[pickedWords.Length];

        for (var i = 0; i < pickedWords.Length; i++)
        {
            translatedWords[i] = wordManager.Translate(pickedWords[i]);
        }

        translatedWords = RandomizeArray(translatedWords);

        for (var i = 0; i < translatedWords.Length; i++)
        {
            Debug.Log(translatedWords[i]);
        }

        var s = 0;
        var e = 0;

        for (var i = 0; i < wordSlots.Length; i++)
        {
            if (wordSlots[i].text == "spanish")
            {
                wordSlots[i].text = pickedWords[s];
                s++;
            }
            else if (wordSlots[i].text == "english")
            {
                wordSlots[i].text = translatedWords[e];
                e++;

            }

        }

    }

    public string[] RandomizeArray(string[] arrayToRandomize)
    {
        System.Random random = new System.Random();
        int n = arrayToRandomize.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            string temp = arrayToRandomize[k];
            arrayToRandomize[k] = arrayToRandomize[n];
            arrayToRandomize[n] = temp;
        }
        return arrayToRandomize;
    }


}

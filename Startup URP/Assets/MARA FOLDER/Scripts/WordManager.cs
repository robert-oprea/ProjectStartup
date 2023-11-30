using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WordManager : MonoBehaviour
{

    public ReadDictionary dictionary;

    public TextMeshPro Text;



    // Start is called before the first frame update
    void Start()
    {
        dictionary = GetComponent<ReadDictionary>();



        Text= FindObjectOfType<TextMeshPro>();
        /*Text.text = dictionary.allWords[random][0];
        */


        // DEBUGS

        /*
         
        var pickedWords = PickWords(4);

        for (var i = 0; i < pickedWords.Length; i++)
        {
            Debug.Log(pickedWords[i]);

        }*/


        var random = Random.Range(0, dictionary.lineNr);
        Debug.Log(dictionary.allWords[random][0] + " means " + Translate(dictionary.allWords[random][0]));



    }

    // Update is called once per frame
    void Update()
    {

    }


    string[] PickWords(int nrOfWords)
    {
        string[] pickedWords = new string[nrOfWords];
        for (var i = 0; i < nrOfWords; i++)
        {
            var random = Random.Range(0, dictionary.lineNr);
            if (NotAlreadyPicked(dictionary.allWords[random][0], pickedWords))
            {
                pickedWords[i] = dictionary.allWords[random][0];

            }

        }

        return pickedWords;

    }


    bool NotAlreadyPicked(string wordToCheck, string[] stringToCheck)
    {
        var ok = true;
        foreach (string word in stringToCheck)
        {
            if (word == wordToCheck)
            {
                ok = false;
            }

        }
        return ok;

    }

    string meaning;

    string Translate(string word)
    {

        for (var i = 0; i < dictionary.lineNr; i++)
        {
            if (dictionary.allWords[i][0] == word)
            {
                meaning = dictionary.allWords[i][1];
            }

        }

        return meaning;

    }



}

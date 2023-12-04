using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WordManager : MonoBehaviour
{

    public ReadDictionary dictionary;

    public TextMeshPro Text;



    void Start()
    {
        // SETUP

        dictionary = GetComponent<ReadDictionary>();





        // DEBUGS

        /*Text.text = dictionary.allWords[random][0];
        
         
        var pickedWords = PickWords(4);

        for (var i = 0; i < pickedWords.Length; i++)
        {
            Debug.Log(pickedWords[i]);

        }

        var random = Random.Range(0, dictionary.lineNr);
        Debug.Log(dictionary.allWords[random][0] + " means " + Translate(dictionary.allWords[random][0]));
        */
    }


    public string[] PickWords(int nrOfWords)
    {
        //pickes a nr of random words from the array

        string[] pickedWords = new string[nrOfWords];
        for (var i = 0; i < nrOfWords; i++)
        {
            var random = Random.Range(0, dictionary.lineNr);

            // 0 means spanish
            if (NotAlreadyPicked(dictionary.allWords[random][0], pickedWords))
            {
                pickedWords[i] = dictionary.allWords[random][0];
            }
        }
        return pickedWords;
    }


    public bool NotAlreadyPicked(string wordToCheck, string[] stringToCheck)
    {
        //checks if are no 2 words the same in the array

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

    
    string meaning;     //??
    public string Translate(string word)
    {
        //gets the word and looks for it in the database
        //returns the "word next to it" in the array (english)

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadDictionary : MonoBehaviour
{
    public string filename;
    public string[] lines;
    public string[] words;

    public string[][] allWords;
    public int lineNr = 0;




    void Start()
    {
        lines = File.ReadAllLines("Assets/" + filename);
        allWords = new string[lines.Length][];

        //instead of an array maybe use a list
        //startwith an empty list and after each add 
        //List<string[]> allWords List<List<string>>


        foreach (string line in lines)
        {
            words = line.Split(','); // Should work as long as there are no comma's in the Excel sheet  //Debug.Log(words[0] + " is the Spanish word for " + words[1]);

            allWords[lineNr] = words;

            for (var i = 0; i < words.Length; i++)
            {
                //Debug.Log(allWords[lineNr][i]);

            }
            
            //Debug.Log(lineNr);
            lineNr++;

        }

    }

}

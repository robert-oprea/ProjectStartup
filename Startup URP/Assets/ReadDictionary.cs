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
        ReadAndStore();

    }


    void ReadAndStore()
    {
        //get the file
        lines = File.ReadAllLines("Assets/" + filename);
        
        allWords = new string[lines.Length][];


        foreach (string line in lines)
        {
            //split each row by the comma's
            words = line.Split(','); // should work as long as there are no comma's in the Excel sheet  

            //store each split row in a jagged array
            allWords[lineNr] = words;

            lineNr++;

        }
    }

}



//instead of an array we can use a list
//startwith an empty list and after each add 
//List<string[]> allWords List<List<string>>

//Debug.Log(words[0] + " is the Spanish word for " + words[1]);
/*for (var i = 0; i < words.Length; i++)
{
    Debug.Log(allWords[lineNr][i]);

}

Debug.Log(lineNr);
*/

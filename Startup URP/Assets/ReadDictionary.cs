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

        char separator = lines[0].IndexOf(',') >= 0 ? ',' : ';';

        foreach (string line in lines)
        {
            //split each row by the separator
            words = line.Split(separator);  

            //store each split row in a jagged array
            allWords[lineNr] = words;

            lineNr++;

        }
    }

}


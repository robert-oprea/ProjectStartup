using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadDictionary : MonoBehaviour
{
    public string filename;
    public string[] lines;
    public string[] words;

    public string[][] allWords = new string[10][];
    public int lineNr = 0;

    


    // Start is called before the first frame update
    void Start()
    {
        lines = File.ReadAllLines("Assets/"+filename);
        

        foreach (string line in lines)
        {
            words = line.Split(','); // Should work as long as there are no comma's in the Excel sheet  //Debug.Log(words[0] + " is the Spanish word for " + words[1]);

            allWords[lineNr] = words;

            Debug.Log(allWords[lineNr]);
            //ask paul why words don t get assigned to this

            lineNr++;



            /*for (int i = 0; i <= words.Length; i++)
            {
                for (int j = 0; j <= lines.Length; j++)
                {
                    string[,] allWords = new string[words.Length, lines.Length];
                    allWords[i, j] = lines[j];

                }
            }*/


        }

        /*for (int i = 0; i <= lineNr; i++)
        {
            
                Debug.Log(allWords[i] + "\n");

            
        }*/
        



    }

    // Update is called once per frame
    void Update()
    {


    }






}

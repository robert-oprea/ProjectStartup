using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadDictionary : MonoBehaviour
{
    public string filename;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = File.ReadAllLines("Assets/"+filename);
        foreach (string line in lines)
        {
            string[] words = line.Split(','); // Should work as long as there are no comma's in the Excel sheet
            Debug.Log(words[0] + " is the Spanish word for " + words[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

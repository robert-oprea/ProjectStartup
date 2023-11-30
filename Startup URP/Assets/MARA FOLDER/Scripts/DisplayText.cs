using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DisplayText : MonoBehaviour
{

    WordManager wordManager;

    [SerializeField]
    TextMeshPro[] wordSlots;


    // Start is called before the first frame update
    void Start()
    {
        wordManager= GetComponent<WordManager>();

        //Text = FindObjectOfType<TextMeshPro>();
         //Text = GetComponent<TextMeshPro>();


        
        


    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < wordSlots.Length; i++)
        {
            if (wordSlots[i].text == "spanish")
            {
                wordSlots[i].text = wordManager.dictionary.allWords[i][0];
            }else if (wordSlots[i].text == "english")
            {
                wordSlots[i].text = wordManager.dictionary.allWords[i][1];

            }
        }
        

    }



}

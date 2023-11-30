using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DisplayText : MonoBehaviour
{

    WordManager wordManager;

    [SerializeField]
    TextMeshPro[] wordSlots;

    string[] pickedWords;


    // Start is called before the first frame update
    void Start()
    {
        wordManager = GetComponent<WordManager>();

        //Text = FindObjectOfType<TextMeshPro>();
        //Text = GetComponent<TextMeshPro>();

        pickedWords = wordManager.PickWords(3);

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
                wordSlots[i].text = wordManager.Translate(pickedWords[e]);
                e++;

            }

        }



    }

    // Update is called once per frame
    void Update()
    {


    }



}

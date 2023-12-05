using System.Collections;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    private bool isStarted = false;

    private BasicInkExample inkDialogueScript; //reference to the ink dialogue script

    private PlayerController player;

    public void StartQuest()
    {
        if (!isStarted)
        {
            isStarted = true;

            // Quest initialization logic
            Debug.Log("Quest1 started!");

            // StartCoroutine(YourQuestCoroutine()); // If you have asynchronous tasks
        }
    }

    /*
    private IEnumerator YourQuestCoroutine()
    {
        // Coroutine logic for asynchronous tasks
        yield return null;
    }
    */
}

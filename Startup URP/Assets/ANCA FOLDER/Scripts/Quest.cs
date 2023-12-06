using System;
using UnityEngine;

public class Quest
{
    public string questName;
    public bool isStarted;
    public bool isCompleted;

    public Quest(string name, string desc)
    {
        questName = name;
        isStarted = false;
        isCompleted = false;
    }

    public void StartQuest()
    {
        if (!isStarted && !isCompleted)
        {
            isStarted = true;
            Debug.Log("Quest started: " + questName);
            // Additional logic for starting the quest
        }
    }

    public void CompleteQuest()
    {
        if (isStarted && !isCompleted)
        {
            isCompleted = true;
            Debug.Log("Quest completed: " + questName);
            // Additional logic for completing the quest
        }
    }
}

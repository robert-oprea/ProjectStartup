using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{
    // TODO: DontDestroyOnLoad / scene singleton

    /* // set this in inspector
     public Quest[] allQuests;*/

    public static event Action<string> OnQuestStarted;

    private List<Quest> quests = new List<Quest>();

    public void StartQuest(string questName)
    {
        Quest questToStart = GetQuestByName(questName);


        if (questToStart != null && !questToStart.isStarted)
        {
            OnQuestStarted?.Invoke(questName);

            Debug.Log("quest started: " + questName);

            DoQuest(questName);
        }
    }

    private Quest GetQuestByName(string questName)
    {
        return quests.Find(q => q.questName == questName);
    }

    private void DoQuest(string questName)
    {
        switch (questName)
        {
            case "Quest1":
                //GetComponent<Quest1>().Start();
                break;
        }
    }
}

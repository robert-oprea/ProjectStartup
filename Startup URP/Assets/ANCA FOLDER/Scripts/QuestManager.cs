using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //lists of quests in different states
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public List<Quest> failedQuests;

    //adding quests to the active quests list if they havent started before
    public void StartQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest) && !completedQuests.Contains(quest) && !failedQuests.Contains(quest))
        {
            quest.StartQuest();
            activeQuests.Add(quest);
        }
    }

    //updating the progress of each quest based on their objectives
    public void UpdateObjectiveProgress(Quest quest, Objective objective, int amount)
    {
        if (activeQuests.Contains(quest) && !objective.isCompleted) //if the objective has not been completed yet
        {
            objective.currentAmount += amount; //increases amount so its closer to the end target
            if (objective.currentAmount >= objective.targetAmount) //if the target has been reached
            {
                objective.currentAmount = objective.targetAmount;
                objective.isCompleted = true; //objective is complete
                quest.CompleteObjective(objective);
            }
        }
    }

    //manages completed quests
    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            quest.FinishQuest();
            completedQuests.Add(quest);
            activeQuests.Remove(quest);
        }
    }

    //manages failed quests
    public void FailQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            quest.FailQuest();
            failedQuests.Add(quest);
            activeQuests.Remove(quest);
        }
    }

    //checking if a quest can be changed from "in progress" to completed
    public void UpdateQuestState(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            if (quest.state == QuestState.InProgress && quest.CheckAllObjectivesCompleted())
            {
                CompleteQuest(quest);
            }
        }
    }
}

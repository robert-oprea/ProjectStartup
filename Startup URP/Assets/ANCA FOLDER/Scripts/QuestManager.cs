using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public List<Quest> failedQuests;

    public void StartQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest) && !completedQuests.Contains(quest) && !failedQuests.Contains(quest))
        {
            quest.StartQuest();
            activeQuests.Add(quest);
        }
    }

    public void UpdateObjectiveProgress(Quest quest, Objective objective, int amount)
    {
        if (activeQuests.Contains(quest) && !objective.isCompleted)
        {
            objective.currentAmount += amount;
            if (objective.currentAmount >= objective.targetAmount)
            {
                objective.currentAmount = objective.targetAmount;
                objective.isCompleted = true;
                quest.CompleteObjective(objective);
            }
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            quest.FinishQuest();
            completedQuests.Add(quest);
            activeQuests.Remove(quest);
        }
    }

    public void FailQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            quest.FailQuest();
            failedQuests.Add(quest);
            activeQuests.Remove(quest);
        }
    }

    public void UpdateQuestState(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            if (quest.state == QuestState.InProgress && quest.CheckAllObjectivesCompleted())
            {
                CompleteQuest(quest);
            }
            // Additional checks and updates based on quest state
        }
    }
}

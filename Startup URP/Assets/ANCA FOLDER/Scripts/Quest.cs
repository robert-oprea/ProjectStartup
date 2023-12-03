using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Objective
{
    public string description;
    public int targetAmount;
    public int currentAmount;
    public bool isCompleted;
}

[Serializable]
public class Quest
{
    public string questName;
    public string description;
    public List<Objective> objectives;
    public QuestState state;
    public List<QuestRequirement> requirements;

    public void StartQuest()
    {
        if (state == QuestState.NotStarted && CheckQuestRequirements())
        {
            state = QuestState.InProgress;
        }
    }

    public void CompleteObjective(Objective objective)
    {
        if(objectives.Contains(objective) && !objective.isCompleted)
        {
            objective.isCompleted = true;
            if (CheckAllObjectivesCompleted())
            {
                FinishQuest();
            }
        }
    }

    public void FinishQuest()
    {
        state = QuestState.Completed;
    }

    public void FailQuest()
    {
        state = QuestState.Failed;
    }

    public bool CheckAllObjectivesCompleted()
    {
        return objectives.All(obj => obj.isCompleted);
    }

    private bool CheckQuestRequirements()
    {
        // Check if all quest requirements are met
        return requirements.All(req => req.IsMet());
    }
}


[Serializable]
public class QuestRequirement
{
    // Implement specific requirements for starting a quest
    // For example, player level, completed quests, etc.

    public bool IsMet()
    {
        // Check if the requirement is met
        return true;
    }
}
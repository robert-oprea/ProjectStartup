using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//objective within a quest
[Serializable]
public class Objective
{
    public string description;
    public int targetAmount;
    public int currentAmount;
    public bool isCompleted;
}

[Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public List<Objective> objectives; //list of objectives within a quest
    public QuestState state; //the state of the quest
    public List<QuestRequirement> requirements; //list of requirements before activating the quest


    //start the quest if it hasnt already started and all requirements are met
    public void StartQuest()
    {
        if (state == QuestState.NotStarted && CheckQuestRequirements())
        {
            state = QuestState.InProgress;
        }
    }

    //completes an objective
    public void CompleteObjective(Objective objective)
    {
        if(objectives.Contains(objective) && !objective.isCompleted)
        {
            objective.isCompleted = true;
            if (CheckAllObjectivesCompleted()) //checking if all objectives in the quest are completed
            {
                FinishQuest(); //quest is completed
            }
        }
    }

    //quest state complete
    public void FinishQuest()
    {
        state = QuestState.Completed;
    }

    //quest state failed - for level system
    public void FailQuest()
    {
        state = QuestState.Failed;
    }

    //checking if all quest objectives have been completed
    public bool CheckAllObjectivesCompleted()
    {
        return objectives.All(obj => obj.isCompleted);
    }

    //checking if all quest requirements are met
    private bool CheckQuestRequirements()
    {
        return requirements.All(req => req.IsMet());
    }
}

[Serializable]
public class QuestRequirement
{
    //for future level system

    public bool IsMet()
    {
        //checking if a requirement is met
        return true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : QuestStep
{
    //static info
    public QuestInfoSO info;

    //state info
    public QuestState state;

    private int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.requirementsNotMet;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < info.questStepPrefabs.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStep();
        if (questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStep()
    {
        GameObject questStepPrefabs = null;
        if (CurrentStepExists())
        {
            questStepPrefabs = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.Log("no current step");
        }
        return questStepPrefabs;
    }
}

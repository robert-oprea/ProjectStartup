using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
        }
    }
}

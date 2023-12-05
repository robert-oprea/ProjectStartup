using System;

[Serializable]
public class Quest
{
    public string questName;
    public bool isStarted;

    public Quest(string name)
    {
        this.questName = name;
        this.isStarted = false;
    }
}

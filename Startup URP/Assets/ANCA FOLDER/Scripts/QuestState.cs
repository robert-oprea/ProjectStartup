public enum QuestState
{
    NotStarted = 0,
    InProgress = 1 << 0,
    Completed = 1 << 1,
    Failed = 1 << 2,
}

using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string npcName;
    public Transform emptyChildTransform;

    private void Start()
    {
        // Register the NPC with the NPCManager when it starts
        NPCManager.Instance.RegisterNPC(this);
    }
}

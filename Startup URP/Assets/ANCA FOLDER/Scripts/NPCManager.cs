using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    public List<NPCController> npcList = new List<NPCController>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterNPC(NPCController npc)
    {
        npcList.Add(npc);
    }

    public void UnregisterNPC(NPCController npc)
    {
        npcList.Remove(npc);
    }

}

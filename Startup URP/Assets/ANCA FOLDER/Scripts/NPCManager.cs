using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance; //creating an instance of the npc manager

    public List<NPCController> npcList = new List<NPCController>(); //list where we store all npcs

    //assigning instance to the manager
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

    //adding npc from the list
    public void RegisterNPC(NPCController npc)
    {
        npcList.Add(npc);
    }

    //removing npc from the list
    public void UnregisterNPC(NPCController npc)
    {
        npcList.Remove(npc);
    }

}

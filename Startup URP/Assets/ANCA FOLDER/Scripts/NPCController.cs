using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("NPC Information")]
    public string npcName;
    public List<TextAsset> inkStories;
   
    public Transform emptyChildTransform;

    private BasicInkExample inkDialogueScript;

    private void Start()
    {
        //register the npc with the npc manager when it starts
        NPCManager.Instance.RegisterNPC(this);

        //getting the ink dialogue script attached to this npc
        inkDialogueScript = GetComponent<BasicInkExample>();

    }

    public void PlayerInteraction()
    {
        TextAsset selectedStory = ChooseStory();

        //setting the npc-specific json file for the ink dialogue script
        if (inkDialogueScript != null)
        {
            inkDialogueScript.SetStoryJSON(selectedStory);
            inkDialogueScript.StartStory();
        }
        else
        {
            Debug.Log("script not found");
        }
    }

    public TextAsset ChooseStory()
    {
        if(inkStories.Count == 1)
        {
            return inkStories[0];
        }
        else
        {
            //logica
            return inkStories[0];
        }
    }
}

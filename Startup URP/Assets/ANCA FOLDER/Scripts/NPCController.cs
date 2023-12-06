using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("NPC Information")]
    public string npcName;
    public TextAsset inkJSONAsset; //assign a json file to each npc in the editor
    public Transform emptyChildTransform;

    private BasicInkExample inkDialogueScript;

    private void Start()
    {
        //register the npc with the npc manager when it starts
        NPCManager.Instance.RegisterNPC(this);

        //getting the ink dialogue script attached to this npc
        inkDialogueScript = GetComponent<BasicInkExample>();

        //setting the npc-specific json file for the ink dialogue script
        if (inkDialogueScript != null && inkJSONAsset!=null)
        {
            inkDialogueScript.SetStoryJSON(inkJSONAsset);
        }
    }
}

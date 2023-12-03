using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("NPC Information")]
    public string npcName;
    public TextAsset inkJSONAsset; // Assign the JSON file for each NPC in the Unity Editor
    public Transform emptyChildTransform;

    private BasicInkExample inkDialogueScript;

    private void Start()
    {
        // Register the NPC with the NPCManager when it starts
        NPCManager.Instance.RegisterNPC(this);

        // Get the InkDialogue script attached to this NPC
        inkDialogueScript = GetComponent<BasicInkExample>();

        // Set the NPC-specific JSON file for the InkDialogue script
        if (inkDialogueScript != null && inkJSONAsset != null)
        {
            inkDialogueScript.SetStoryJSON(inkJSONAsset);
        }
    }
}

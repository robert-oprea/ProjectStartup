using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField]
    private BasicInkExample inkDialogueScript; // Reference to the Ink dialogue script
    public KeyCode interactionKey = KeyCode.E;

 
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(interactionKey))
        {
            Debug.Log("E pressed");

            StartDialogue();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        inkDialogueScript.RemoveChildren();
    }

    private void StartDialogue()
    {
        // Trigger the dialogue or any other actions you want when interacting with the NPC
        inkDialogueScript.StartStory();
    }
}
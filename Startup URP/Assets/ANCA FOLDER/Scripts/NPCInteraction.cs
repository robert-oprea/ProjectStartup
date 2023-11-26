using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField]
    private BasicInkExample inkDialogueScript; // Reference to the Ink dialogue script

    [SerializeField]
    private KeyCode interactionKey = KeyCode.E;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Display an interaction prompt or button, e.g., show "Press E to Interact" on the UI

            if (Input.GetKeyDown(interactionKey))
            {
                Debug.Log("e pressed");
                // Trigger the dialogue when the player presses the interaction key
                inkDialogueScript.StartStory();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inkDialogueScript.RemoveChildren();
        }
    }
}

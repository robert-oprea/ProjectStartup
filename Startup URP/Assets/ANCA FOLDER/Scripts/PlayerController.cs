using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private BasicInkExample inkDialogueScript; // Reference to the Ink dialogue script

    public KeyCode interactKey = KeyCode.E;

    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;

    public CameraSwitcher cameraSwitcher; // Reference to the CameraSwitcher script

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers;

    private bool canMove = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for left mouse button click
        if (canMove && Input.GetMouseButtonDown(0))
        {
            // Check if the pointer is over a UI element using raycasting
            MoveToMouseClick();
        }
    }

    public void MoveToMouseClick()
    {
        if (cameraSwitcher.ActiveCamera != null)
        {
            // Check if the second camera is active
            if (cameraSwitcher.ActiveCamera == cameraSwitcher.otherCamera)
            {
                // Player is not allowed to move when the second camera is active
                return;
            }

            RaycastHit hit;
            Ray ray = cameraSwitcher.ActiveCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, clickableLayers))
            {
                if (hit.collider != null)
                {
                    agent.destination = hit.point;
                }
            }
        }
    }

    private void TeleportPlayerToNPC(NPCController npc)
    {
        transform.position = npc.emptyChildTransform.position;
        agent.ResetPath();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            agent.ResetPath();

            NPCController npc = collision.gameObject.GetComponent<NPCController>();

            if (npc != null && Input.GetKeyDown(interactKey))
            {
                canMove = false;
                StartCoroutine(cameraSwitcher.SwitchCamerasSmoothCoroutine());

                TeleportPlayerToNPC(npc);

                StartDialogue();
                Debug.Log("test test");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //EndDialogue();
        canMove = true;
    }

    private void StartDialogue()
    {
        // Trigger the dialogue or any other actions you want when interacting with the NPC
        inkDialogueScript.StartStory();
    }

    private void EndDialogue()
    {
        inkDialogueScript.RemoveChildren();
    }

    void SetAnimation()
    {
        // Implement animation logic if needed
    }
}

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    private BasicInkExample inkDialogueScript; // Reference to the Ink dialogue script

    public KeyCode interactKey = KeyCode.E;

    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;

    public CameraSwitcher cameraSwitcher; // Reference to the CameraSwitcher script

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers;


    private bool canMove = true;

    private void Start()
    {
        inkDialogueScript = FindObjectOfType<BasicInkExample>();
        if (inkDialogueScript == null)
        {
            Debug.LogError("BasicInkExample script not found in the scene!");
        }
    }

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
                Debug.Log("Interacted with NPC: " + npc.npcName);
                canMove = false;
                StartCoroutine(cameraSwitcher.SwitchCamerasSmoothCoroutine());

                // Set the story associated with the NPC
                inkDialogueScript.SetStoryJSON(npc.inkJSONAsset);

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
        if (inkDialogueScript != null)
        {
            inkDialogueScript.StartStory();
        }
        else
        {
            Debug.LogError("inkDialogueScript is not assigned!");
        }
    }

    private void EndDialogue()
    {
        if (inkDialogueScript != null)
        {
            inkDialogueScript.RemoveChildren();
        }
        else
        {
            Debug.LogError("inkDialogueScript is not assigned!");
        }
    }

    void SetAnimation()
    {
        // Implement animation logic if needed
    }

   
}

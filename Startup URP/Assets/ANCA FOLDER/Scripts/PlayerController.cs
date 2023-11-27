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

    public Camera mainCamera;
    public Camera otherCamera;

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers;

    private Camera activeCamera;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        activeCamera = mainCamera;
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the pointer is over a UI element using raycasting
            MoveToMouseClick();
        }
    }

    public void MoveToMouseClick()
    {
        RaycastHit hit;

        if (activeCamera != null)
        {
            Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);

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
                //SwitchCamera();
                otherCamera.enabled = true;
                mainCamera.enabled = false;

                TeleportPlayerToNPC(npc);
                StartDialogue();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        EndDialogue();
    }

    /*private void SwitchCamera()
    {
        if(activeCamera == mainCamera)
        {
            otherCamera.tag = "MainCamera";
            mainCamera.tag = null;
            activeCamera = otherCamera;
        }
        else
        {
            mainCamera.tag = "MainCamera";
            otherCamera.tag = null;
            activeCamera = mainCamera;
        }
    }*/

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

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    private BasicInkExample inkDialogueScript; //reference to the ink dialogue script

    public KeyCode interactKey = KeyCode.E;

    NavMeshAgent agent; //agent component for the nav mesh
    Animator animator;
    Rigidbody rb;

    public CameraSwitcher cameraSwitcher; //reference to the camer switcher script

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers; //object layer for player to know if they can click on an object or not


    private bool canMove = true;

    private float lookRotationSpeed = 8f;

    //finding ink dialogue script to reference
    private void Start()
    {
        inkDialogueScript = FindObjectOfType<BasicInkExample>();
        if (inkDialogueScript == null)
        {
            Debug.LogError("BasicInkExample script not found in the scene");
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
        //checking for left mouse button click
        if (canMove && Input.GetMouseButtonDown(0))
        {
            //checking if the pointer is over a UI element using raycasting
            MoveToMouseClick();
            FaceTarget();
        }
    }

    public void MoveToMouseClick()
    {
        if (cameraSwitcher.ActiveCamera != null)
        {
            //checking if second camera is active
            if (cameraSwitcher.ActiveCamera == cameraSwitcher.otherCamera)
            {
                return; //player is not allowed to move when second camera is active
            }

            RaycastHit hit;
            Ray ray = cameraSwitcher.ActiveCamera.ScreenPointToRay(Input.mousePosition); //raycasting to where the mouse click was on screen

            if (Physics.Raycast(ray, out hit, 100, clickableLayers)) //if the player clicked on the layer we want it to walk on
            {
                if (hit.collider != null)
                {
                    agent.destination = hit.point; //player destination is towards the mouse click
                }
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    //teleporting the player to the empty object child of the npc
    private void TeleportPlayerToNPC(NPCController npc)
    {
        transform.position = npc.emptyChildTransform.position;
        agent.ResetPath(); //resets the path so the player doesnt bump into the object
    }

    //npc logic when player collides with it
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            agent.ResetPath();

            NPCController npc = collision.gameObject.GetComponent<NPCController>();

            if (npc != null && Input.GetKeyDown(interactKey)) //when the player collides with an  npc and they press the interaction key
            {
                Debug.Log("Interacted with NPC: " + npc.npcName);
                canMove = false; //player cannot move towards new mouse click
                StartCoroutine(cameraSwitcher.SwitchToDialogueCamera()); //switching to the second "dialogue" camera

                //setting the story associated with the npc
                inkDialogueScript.SetStoryJSON(npc.inkJSONAsset);

                TeleportPlayerToNPC(npc);

                StartDialogue(); //starts the dialogue scene
                Debug.Log("test test");
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        //EndDialogue();
        canMove = true;
    }

    //calls for the startstory method in the ink script
    private void StartDialogue()
    {
        if (inkDialogueScript != null)
        {
            inkDialogueScript.StartStory();
        }
        else
        {
            Debug.LogError("inkDialogueScript is not assigned");
        }
    }

   /* private void EndDialogue()
    {
        if (inkDialogueScript != null)
        {
            inkDialogueScript.RemoveChildren();
        }
        else
        {
            Debug.LogError("inkDialogueScript is not assigned");
        }
    }*/

    void SetAnimation()
    {
        //for future player animation
    }

   
}

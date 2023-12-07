using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
       private BasicInkExample inkDialogueScript; //reference to the ink dialogue script

    public KeyCode interactKey = KeyCode.E;

    NavMeshAgent agent; //agent component for the nav mesh
    Animator animator;
   
    public CameraSwitcher cameraSwitcher; //reference to the camer switcher script

    private Vector3 lastMovementDirection;

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers; //object layer for player to know if they can click on an object or not


    private bool canMove = true;

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
    }

    //for pc
    void Update()
    {
        //checking for left mouse button click
        if (canMove && Input.GetMouseButtonDown(0))
        {
            //checking if the pointer is over a UI element using raycasting
            MoveToMouseClick();
        }
            
        SetAnimation();
    }

    //for mobile
    /*void Update()
    {
        // Check for touch input
        if (canMove && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if it's the beginning of a touch
            if (touch.phase == TouchPhase.Began)
            {
                // Checking if the pointer is over a UI element using raycasting
                MoveToTouchPosition(touch.position);
            }
        }

        SetAnimation();
    }*/

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
                    FaceTarget();
                }
            }
        }
    }

    /*public void MoveToTouchPosition(Vector2 touchPosition)
    {
        if (cameraSwitcher.ActiveCamera != null)
        {
            // Checking if the second camera is active
            if (cameraSwitcher.ActiveCamera == cameraSwitcher.otherCamera)
            {
                return; // Player is not allowed to move when the second camera is active
            }

            RaycastHit hit;
            Ray ray = cameraSwitcher.ActiveCamera.ScreenPointToRay(touchPosition); // Raycasting to where the touch occurred

            if (Physics.Raycast(ray, out hit, 100, clickableLayers)) // If the player touched the layer we want it to walk on
            {
                if (hit.collider != null)
                {
                    agent.destination = hit.point; // Player destination is towards the touch position
                    FaceTarget();
                }
            }
        }
    }*/

    /*void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
            *//*Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);*//*
    }*/

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;

        // Check if there is movement
        if (direction.magnitude > 0.1f)
        {
            // Update last known movement direction
            lastMovementDirection = new Vector3(direction.x, 0, direction.z);

            // Calculate rotation towards the movement direction
            Quaternion lookRotation = Quaternion.LookRotation(lastMovementDirection);

            // Set the rotation directly
            transform.rotation = lookRotation;
        }
        // If there is no movement, maintain the last known movement direction
        else if (lastMovementDirection.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lastMovementDirection);
            transform.rotation = lookRotation;
        }
    }

    //teleporting the player to the empty object child of the npc
    private void TeleportPlayerToNPC(NPCController npc)
    {
        transform.position = npc.emptyChildTransform.position;
        transform.rotation = Quaternion.LookRotation(npc.emptyChildTransform.forward);

       

        agent.ResetPath(); //resets the path so the player doesnt bump into the object

      /*  Debug.Log(transform.rotation); Debug.Log(npc.transform.rotation);
       // Quaternion lookRotation = Quaternion.RotateTowards(this.transform.rotation, npc.transform.rotation, 180);
        transform.LookAt(npc.transform);*/

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

                //INTERACTION WITH NPCS
                QuestGame questGame = GetComponent<QuestGame>();
                if(npc.npcName == "Juan")
                {
                    if (questGame != null)
                    {
                        questGame.talkedToJuan = true;
                        Debug.Log("juan dami quest");
                    }
                    inkDialogueScript.SetStoryJSON(npc.inkJSONAsset);
                    StartDialogue();
                }

                if (npc.npcName == "Bootcamp")
                {
                    if (questGame != null)
                    {
                        string inkFileName = "Bootcamp";
                        TextAsset inkJSONAsset = Resources.Load<TextAsset>("Ink/" + inkFileName);


                        if (inkJSONAsset != null)
                        {
                            inkDialogueScript.SetStoryJSON(inkJSONAsset);
                            StartDialogue();

                            Debug.Log(inkJSONAsset.name);
                        }
                        else
                        {
                            Debug.LogError("Ink JSON Asset is null!");
                        }
                    }
                    else
                    {
                        Debug.LogError("QuestGame is null!");
                    }
                }


                if (npc.npcName == "Marabela")
                {
                    Debug.Log("Checking Marabela");
                    if (questGame != null)
                    {
                        string inkFileName = questGame.talkedToJuan ? "TalkToJuan" : "NoTalkToJuan";
                        TextAsset inkJSONAsset = Resources.Load<TextAsset>("Ink/" + inkFileName);
                        

                        if (inkJSONAsset != null)
                        {
                            inkDialogueScript.SetStoryJSON(inkJSONAsset);
                            inkDialogueScript.StartStory();

                            Debug.Log(inkJSONAsset.name);
                        }
                        else
                        {
                            Debug.LogError("Ink JSON Asset is null!");
                        }
                    }
                    else
                    {
                        Debug.LogError("QuestGame is null!");
                    }
                }


                Debug.Log("Interacted with NPC: " + npc.npcName);
                
                canMove = false; //player cannot move towards new mouse click
                StartCoroutine(cameraSwitcher.SwitchToDialogueCamera()); //switching to the second "dialogue" camera

                //setting the story associated with the npc
                //inkDialogueScript.SetStoryJSON(npc.inkJSONAsset);

                TeleportPlayerToNPC(npc);

                //StartDialogue(); //starts the dialogue scene
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

    void SetAnimation()
    {
        bool isMoving = agent.velocity.sqrMagnitude > 0;

        // Set the "moving" parameter in the Animator
        animator.SetBool("moving", isMoving);
    }

   
}

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 5f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the pointer is over a UI element using raycasting
            if (!IsPointerOverUI())
            {
                MoveToMouseClick();
            }
        }

        FaceTarget();
    }

    void MoveToMouseClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            if (hit.collider != null)
            {
                agent.destination = hit.point;                
            }

            if(!hit.collider.CompareTag("Ground"))
            {
                agent.destination = hit.point ;
            }
        }

    }

    bool IsPointerOverUI()
    {
        // Implement your own raycasting logic to check if the pointer is over a UI element
        // Return true if the pointer is over a UI element, otherwise, return false
        // This depends on your specific UI setup and requirements
        return false;
    }

    void FaceTarget()
    {
        // Implement face target logic if needed
    }

    void SetAnimation()
    {
        // Implement animation logic if needed
    }
}

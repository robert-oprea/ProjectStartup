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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            agent.destination = hit.point;
        }
    }

    bool IsPointerOverUI()
    {
        // Implement your own raycasting logic to check if the pointer is over a UI element
        // For example, you can use Physics.Raycast, EventSystem.RaycastAll, or other techniques
        // Return true if the pointer is over a UI element, otherwise, return false
        // This depends on your specific UI setup and requirements
        return false;
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("SELECTABLE") && collision.gameObject.GetComponent<Outline>().enabled == true)
       

            {
                agent.destination = agent.transform.position;
        }
    }

    void SetAnimation()
    {
        // Implement animation logic if needed
    }
}

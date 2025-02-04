using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves

    [SerializeField] Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveManager();
        InteractableManager();

    }


    void MoveManager()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed;

        // Apply movement to the player's Rigidbody
        rb.linearVelocity = movement;
    }

    

    float interactionRadius = 2f; // Maximum interaction distance
    public LayerMask interactableLayer; // LayerMask for interactable objects
    public List<GameObject> interactableObjects; // List of interactable objects (could be set in the Inspector)
    private GameObject closestInteractable = null;

    void InteractableManager()
    {
        // Update the closest interactable object
        UpdateClosestInteractableObject();

        // Interact with the closest object when the player presses the interact key (F)
        if (closestInteractable != null)
        {
            IInteractable interactable = closestInteractable.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }

    void UpdateClosestInteractableObject()
    {
        closestInteractable = null;
        float closestDistance = interactionRadius;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayer);

        if (colliders.Length == 0)
        {
            //Debug.Log("No objects found in range.");
        }

        foreach (Collider2D collider in colliders)
        {
            //Debug.Log("Detected: " + collider.gameObject.name); // Debugging

            float distanceToPlayer = Vector2.Distance(transform.position, collider.transform.position);

            if (distanceToPlayer <= closestDistance)
            {
                if (closestInteractable != null)
                {
                    closestInteractable.GetComponent<ObjectScript>().offUserIndicator(); // Turn off previous

                }

                closestInteractable = collider.gameObject;
                closestInteractable.GetComponent<ObjectScript>().onUserIndicator(); // Highlight closest
                closestDistance = distanceToPlayer;
            }
            else
            {
                collider.gameObject.GetComponent<ObjectScript>().offUserIndicator(); // Ensure others are off
            }
        }
    }

}

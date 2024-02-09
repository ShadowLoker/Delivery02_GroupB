using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 1f;
    private PlayerInput input;

    public void SetInput(PlayerInput input)
    {
        this.input = input;
    }
    private void Start()
    {
        

        if (input == null)
        {
            Debug.LogError("InteractionInput reference is not set on PlayerInteraction.");
        }
    }

    private void Update()
    {
        if (input.GetInteractionInput())
        {
            InteractWithNearbyObject();
        }
    }

    private void InteractWithNearbyObject()
    {
        Debug.Log("Interacting with nearby object");
        // Get all colliders within the interaction range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);

        foreach (Collider2D collider in colliders)
        {
            // Check if the object is interactable
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Interact with the object
                interactable.Interact();
                break; // Exit the loop after interacting with the first interactable object
            }
        }
        
    }
}

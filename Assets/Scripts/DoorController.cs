using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        // Toggle the door open or closed
        isOpen = !isOpen;

        if (isOpen)
        {
            // Open the door by rotating it 90 degrees
            transform.Rotate(0, 0, 90);
        }
        else
        {
            // Close the door by rotating it back -90 degrees
            transform.Rotate(0, 0, -90);
        }
    }
}

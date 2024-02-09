using UnityEngine;
public class GameManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerInteraction playerInteraction;

    private void Start()
    {
        playerInput = new PlayerInput();

        playerMovement.SetInput(playerInput);
        playerInteraction.SetInput(playerInput);
    }
}

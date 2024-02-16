using UnityEngine;
public class GameManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    private void Start()
    {
        playerInput = new PlayerInput();

        playerMovement.SetInput(playerInput);
    }
}

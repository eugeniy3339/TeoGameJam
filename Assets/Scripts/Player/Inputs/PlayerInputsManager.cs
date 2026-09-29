using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsManager : MonoBehaviour
{
    private PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement.moveInputs = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        movement.JumpIfCanTo();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsManager : MonoBehaviour
{
    private PlayerMovement movement;
    private SwingingManager swingingManager;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        swingingManager = GetComponent<SwingingManager>();
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

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        swingingManager.swing(context.started);
    }
}

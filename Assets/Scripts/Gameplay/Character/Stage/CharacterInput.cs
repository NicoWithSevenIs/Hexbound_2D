using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterManager))]
public partial class CharacterInput : MonoBehaviour
{

    [SerializeField] private float Hold_Sensitivity = 0.625f;

    private CharacterController character_controller;
    private CharacterManager character_manager;

    private void Awake()
    {
        character_manager = GetComponent<CharacterManager>();
        character_controller = GetComponent<CharacterController>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        float axis = context.ReadValue<float>();
        character_controller.SetMovementAxis(axis);
    }

    public void Switch_Path(InputAction.CallbackContext context)
    {

    }

    public void Switch_Characters(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("switching characters");
            character_manager.SwitchCharacters();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            character_controller.SetJumpFlag();
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            var cursor_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var char_pos = character_controller.transform.position;
            var dir = (cursor_pos - char_pos).normalized;
            character_controller.SetDashFlag(dir);
        }
    }

    public void Basic_Attack(InputAction.CallbackContext context)
    {

    }

    public void Plunge_Attack(InputAction.CallbackContext context) 
    {

    }

    public void Base_Active(InputAction.CallbackContext context)
    {
    }

    public void Path_Active(InputAction.CallbackContext context)
    {
    }

    public void Ultimate(InputAction.CallbackContext context)
    {
    }

    public void Interact(InputAction.CallbackContext context)
    {
    }

 
}


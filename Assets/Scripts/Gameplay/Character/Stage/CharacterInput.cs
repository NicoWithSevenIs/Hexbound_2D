using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterManager))]
public partial class CharacterInput : MonoBehaviour, IOnCharacterSwitched, IOnCharacterLoaded
{

    [SerializeField] private float Hold_Sensitivity = 0.625f;

    private CharacterMovementController current_controller;
    private CharacterManager character_manager;

    private void Awake()
    {
        character_manager = GetComponent<CharacterManager>();
    }

    public void Switch_Characters(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("switching characters");
            character_manager.SwitchCharacters();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        float axis = context.ReadValue<float>();
        current_controller.SetMovementAxis(axis);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            current_controller.SetJumpFlag();
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            var cursor_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var char_pos = current_controller.transform.position;
            var dir = (cursor_pos - char_pos).normalized;
            Debug.Log(dir);
            current_controller.SetDashFlag(dir);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
    }

}

#region Attack Input
public partial class CharacterInput
{
    public void Basic_Attack(InputAction.CallbackContext context)
    {

    }

    public void Plunge_Attack(InputAction.CallbackContext context)
    {

    }

    #region Ability Input

    public void Base_Active(InputAction.CallbackContext context)
    {
    }

    public void Switch_Path(InputAction.CallbackContext context)
    {

    }

    public void Path_Active(InputAction.CallbackContext context)
    {
    }

    public void Ultimate(InputAction.CallbackContext context)
    {
    }

    #endregion Ability Input
}
#endregion Attack Input

#region Interface

public partial class CharacterInput
{
    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        current_controller = entering.GetComponent<CharacterMovementController>();
    }
    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        current_controller = character1.GetComponent<CharacterMovementController>();
    }
}

#endregion

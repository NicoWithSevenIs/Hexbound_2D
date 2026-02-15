using UnityEngine;
using UnityEngine.InputSystem;
using Event_Args = System.Collections.Generic.Dictionary<string, object>;

[RequireComponent(typeof(CharacterManager))]
public partial class CharacterInput : MonoBehaviour, IOnCharacterSwitched, IOnCharacterLoaded
{

    [SerializeField] private float Hold_Sensitivity = 0.625f;

    private CharacterController current_controller;
 
    public void Move(InputAction.CallbackContext context)
    {
        float axis = context.ReadValue<float>();
        current_controller.SetMovementAxis(axis);
    }

    public void Switch_Path(InputAction.CallbackContext context)
    {
    }
    public void Jump(InputAction.CallbackContext context)
    {

    }
    public void Dash(InputAction.CallbackContext context)
    {
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

#region Interface

public partial class CharacterInput
{
    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        current_controller = entering.GetComponent<CharacterController>();
    }
    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        Debug.Log("Loaded");
        current_controller = character1.GetComponent<CharacterController>();
    }
}

#endregion

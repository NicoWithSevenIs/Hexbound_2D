using UnityEngine;
using UnityEngine.InputSystem;
using Event_Args = System.Collections.Generic.Dictionary<string, object>;

[RequireComponent(typeof(CharacterManager))]
public partial class CharacterInput : MonoBehaviour
{

    [SerializeField] private float Hold_Sensitivity = 0.625f;


    public void Move(InputAction.CallbackContext context)
    {
    }

    public void Switch_Path(InputAction.CallbackContext context)
    {
    }
    public void Jump(InputAction.CallbackContext context)
    {

    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Dashing");
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


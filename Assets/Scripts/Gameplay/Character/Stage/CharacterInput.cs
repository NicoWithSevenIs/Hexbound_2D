using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterManager))]
public partial class CharacterInput : MonoBehaviour, IOnCharacterSwitched, IOnCharacterLoaded
{


    private CharacterMovementController current_movement_controller;
    private CharacterCombatController current_combat_controller;
    private CharacterManager character_manager;

    private void Awake()
    {
        character_manager = GetComponent<CharacterManager>();
    }

    public void Switch_Characters(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            character_manager.SwitchCharacters();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        float axis = context.ReadValue<float>();
        current_movement_controller.SetMovementAxis(axis);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            current_movement_controller.SetJumpFlag();
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            var cursor_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var char_pos = current_movement_controller.transform.position;
            var dir = (cursor_pos - char_pos).normalized;
            current_movement_controller.SetDashFlag(dir);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
    }

}

#region Attack Input
public partial class CharacterInput
{
    [Serializable]
    private class AttackInput
    {
        public bool down = false;
        public float input_time = 0;

        public void Tick()
        {
            if (down)
            {
                input_time += Time.deltaTime;   
            }
        }
    }

    private AttackInput Basic_Attack_Input = new();
    private AttackInput Plunge_Attack_Input = new();

    private void Update()
    {
        Basic_Attack_Input.Tick();
        Plunge_Attack_Input.Tick();
    }

    public void Basic_Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Basic_Attack_Input.down = true;
        }else if (context.canceled)
        {
            Basic_Attack_Input.down= false;
            current_combat_controller.TriggerBasicAttack(Basic_Attack_Input.input_time);
            Basic_Attack_Input.input_time = 0;
        }
    }

    public void Plunge_Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Plunge_Attack_Input.down = true;
        }
        else if (context.canceled)
        {
            Plunge_Attack_Input.down = false;
            current_combat_controller.TriggerPlunge(Plunge_Attack_Input.input_time);
            Plunge_Attack_Input.input_time = 0;
        }
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
        current_movement_controller = entering.GetComponent<CharacterMovementController>();
        current_combat_controller = entering.GetComponent<CharacterCombatController>();
    }
    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        current_movement_controller = character1.GetComponent<CharacterMovementController>();
        current_combat_controller = character1.GetComponent<CharacterCombatController>();
    }
}

#endregion

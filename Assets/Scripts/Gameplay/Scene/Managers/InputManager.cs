using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Rendering.CameraUI;



/*Notes:
    For Modular Implementation, each character should have an interface based input scheme 
    which should work together with an input buffer system in the input buffer branch
    ex.
        For characters with no heavy attacks, all attacks should be determined on input start rather than on cancel.
 */

[RequireComponent(typeof(CharacterManager))]
public class InputManager : MonoBehaviour, IOnCharacterSwitched, IOnCharacterLoaded
{
    #region References
        private CharacterManager character_manager;
        private CharacterController current_controller;
    #endregion



    [Header("Config")]
    [SerializeField] private float hold_treshold = 1f;
    private float hold_time = -1f;

    private void Awake()
    {
        character_manager = GetComponent<CharacterManager>();
    }


    public void Move(InputAction.CallbackContext context)
    {
        float axis = context.ReadValue<float>();
        current_controller.MovementAxis = axis;
    }

    ///

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            current_controller.InitiateAction(PlayerActionType.JUMP);
        }
    }

    public void AerialHover(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            current_controller.IsHovering = true;
        }

        if (context.canceled)
        {
            current_controller.IsHovering = false;
        }
    }


    public void Plunge_Attack(InputAction.CallbackContext context)
    {

    }

    ///

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            var prox = GetComponent<MVP_ProximityAttack>();
            prox.Attack();
            current_controller.InitiateAction(PlayerActionType.ATTACK);
        }
    }

    ///


    public void Base_Active(InputAction.CallbackContext context)
    {

    }

    public void Ultimate(InputAction.CallbackContext context)
    {
    }

    ///

    public void Switch_Stratum(InputAction.CallbackContext context)
    {

        //var dir = -(int)Mathf.Sign(context.ReadValue<float>());
    }


    public void Stratum_Active(InputAction.CallbackContext context)
    {
    }


    ///

    public void Switch_Characters(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            character_manager.SwitchCharacters();
        }
    }


    ///
    public void Interact(InputAction.CallbackContext context)
    {
    }


    ///
    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        current_controller = entering.GetComponent<CharacterController>();
    }

    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        current_controller = character1.GetComponent<CharacterController>();
    }

}

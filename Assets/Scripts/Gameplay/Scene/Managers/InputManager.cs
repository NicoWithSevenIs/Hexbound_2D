using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Rendering.CameraUI;



[RequireComponent(typeof(CharacterManager))]
public class InputManager : MonoBehaviour, IOnCharacterSwitched, IOnCharacterLoaded
{
    #region References
        private CharacterManager character_manager;
        private CharacterController current_controller;
    #endregion

    private Queue<BufferedInput> input_buffer = new();
    //private Dictionary<BufferedInputType, BufferedInput> held_inputs = new();
    private Timer poll_timer; 

    #region Getters

    public List<BufferedInput> InputBuffer { get => new(input_buffer); }
    public int MaxBuffer { get => max_buffer; }
    public float InputLifetime { get => max_input_lifetime; }

    #endregion

    [Header("Config")]
    [SerializeField] private float poll_interval = 0.8f;
    [SerializeField] private int max_buffer = 5;
    [SerializeField] private float hold_treshold = 1f;
    [SerializeField] private float max_input_lifetime = 3f;

    private void Awake()
    {
        character_manager = GetComponent<CharacterManager>();
        poll_timer = new Timer(poll_interval, OnInputBufferPoll);
    }

    #region Buffered Actions
        public void Jump(InputAction.CallbackContext context)
        {
            EnqueueBufferedInput(BufferedInputType.JUMP, context);
        }

        public void Attack(InputAction.CallbackContext context)
        {
            EnqueueBufferedInput(BufferedInputType.ATTACK, context);
        }

        public void Base_Active(InputAction.CallbackContext context)
        {
            EnqueueBufferedInput(BufferedInputType.BASE_ACTIVE, context);
        }


        public void Stratum_Active(InputAction.CallbackContext context)
        {
            EnqueueBufferedInput(BufferedInputType.STRATUM_ACTIVE, context);
        }
    #endregion

    private void EnqueueBufferedInput(BufferedInputType input_type, InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            var inputs = new List<BufferedInput>(input_buffer);
            foreach (var input in inputs)
            {
                if (input.type == input_type)
                    input.is_held = false;
            }
            return;
        }

        if (context.started)
        {
            var success = current_controller.PollBufferedAction(input_type);
            if(!success && input_buffer.Count < max_buffer)
            {
                var input = new BufferedInput()
                {
                    type = input_type,
                    input_time = 0,
                    lifetime = 0,
                    is_held = true
                };
                input_buffer.Enqueue(input);
            }
        }
    }

    private void OnInputBufferPoll()
    {
        BufferedInput head;
        input_buffer.TryPeek(out head);

        while (head != null && head.lifetime >= max_input_lifetime)
        {
            input_buffer.Dequeue();
            input_buffer.TryPeek(out head);
        }

        if (head == null || head.is_held)
            return;

        var success = current_controller.PollBufferedAction(head.type);
        if(success)
            input_buffer.Dequeue();
    }

    private void Update()
    {
        poll_timer.Tick();
        foreach(var input in input_buffer)
        {
            if (!input.is_held)
            {
                input.lifetime += Time.deltaTime;
                continue;
            }
               
            if(input.input_time <= hold_treshold)
            {
                input.input_time += Time.deltaTime; 
            }
            else
            {
                input.is_held = false;
            }
        }
    }

    //Non-buffered actions aside from Move will clear the input buffer
    #region Non-buffered Actions
        public void Move(InputAction.CallbackContext context)
        {
            float axis = context.ReadValue<float>();
            current_controller.MovementAxis = axis;
        }

        public void Interact(InputAction.CallbackContext context)
        {
        }

        public void Switch_Characters(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                character_manager.SwitchCharacters();
            }
        }

        public void Plunge_Attack(InputAction.CallbackContext context)
        {

        }

        public void Ultimate(InputAction.CallbackContext context)
        {
        }
        public void Switch_Stratum(InputAction.CallbackContext context)
        {
  
            //var dir = -(int)Mathf.Sign(context.ReadValue<float>());
        }


    #endregion

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        current_controller = entering.GetComponent<CharacterController>();
    }

    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        current_controller = character1.GetComponent<CharacterController>();
    }

}

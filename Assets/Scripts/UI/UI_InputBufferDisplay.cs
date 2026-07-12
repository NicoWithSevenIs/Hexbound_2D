using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputBufferDisplay : MonoBehaviour
{
    [SerializeField] private InputManager input_manager;
    [SerializeField] private List<GameObject> input_objects;


    private void Update()
    {
        int count = input_manager.InputBuffer.Count;

        foreach(var input_object in input_objects)
        {
            input_object.SetActive(false);
        }

        for (int i = 0; i < count; i++) 
        {
            input_objects[i].SetActive(true);

            var action_type = input_objects[i].GetComponentInChildren<TextMeshProUGUI>();
            var lifetime = input_objects[i].transform.Find("Lifetime").GetComponent<Image>();
            var outline = input_objects[i].GetComponentInChildren<Outline>();

            outline.enabled = input_manager.InputBuffer[i].is_held;
            action_type.text = input_manager.InputBuffer[i].type.ToString();
            lifetime.fillAmount = 1 - (input_manager.InputBuffer[i].lifetime / input_manager.InputLifetime);
        }   


    }
}

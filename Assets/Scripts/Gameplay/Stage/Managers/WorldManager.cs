using System.Collections.Generic;
using UnityEngine;

public partial class WorldManager : SingletonBehaviour<WorldManager>
{
    
    [SerializeField] private GameObject debug_prefab;


    [SerializeField] private CharacterEvents character;


    private void Start()
    {
        RegisterWorld();
    }

    public void CreateEntity(GameObject prefab, Vector2? pos = null)
    {
        var instance = Instantiate(prefab);
        instance.transform.position = pos == null ? Vector2.zero : pos.Value;
        HookUpCharacterEvents(instance);
    }

    public void RegisterWorld()
    {
        List<Transform> children = new(GetComponentsInChildren<Transform>());
        children.RemoveAt(0);

        foreach (Transform child in children)
        {
            HookUpCharacterEvents(child.gameObject);
        }

    }

    public void HookUpCharacterEvents(GameObject instance)
    {
        character.Register<IOnCharacterDefeated>(instance);
    }

}



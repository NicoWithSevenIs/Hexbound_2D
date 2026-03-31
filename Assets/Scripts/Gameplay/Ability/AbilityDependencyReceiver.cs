using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityDependencyReceiver : MonoBehaviour
{
    [SerializeField]
    [SerializedDictionary("Ability Name", "Ability GameObject")]
    private SerializedDictionary<string, GameObject> _dependencies = new();

    public void SetDependency(Ability dependency)
    {
        _dependencies[dependency.AbilityName] = dependency.gameObject;
    }

    public void RemoveDependency(Ability dependency)
    {
        _dependencies.Remove(dependency.AbilityName);
    }

    public void Clear()
    {
        _dependencies.Clear();
    }

    public GameObject this[string key]
    {
        get => _dependencies[key];
    }
}

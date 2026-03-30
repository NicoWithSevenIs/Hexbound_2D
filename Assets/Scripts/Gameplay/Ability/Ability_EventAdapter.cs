using System;
using System.Collections.Generic;
using UnityEngine;

public class Ability_EventAdapter : MonoBehaviour, IEventAdapter
{

    public class ListenerData
    {

    }


    private Dictionary<IEventAdapterListener, Action> event_bus = new();

    public void RegisterAbility(GameObject game_object)
    {
        var ability = game_object.GetComponent<Ability>();
        if(ability != null){
            ability.Initialize(this);
        }
    }

    public bool TryRegister(IEventAdapterListener listener, Type[] events)
    {


        return true;
    }

    public bool TryUnregister(IEventAdapterListener listener)
    {
        throw new NotImplementedException();
    }

    public void OnEnable()
    {
        
    }
}

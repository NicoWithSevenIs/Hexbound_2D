

using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

public interface IAbilityComponent 
{
    public void Initialize(SerializedDictionary<string, List<Multiplier>> multipliers);
    public void TriggerEffect();

}
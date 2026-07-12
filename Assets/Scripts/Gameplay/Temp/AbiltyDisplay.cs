using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AbiltyDisplay : MonoBehaviour, IOnStratumSwitched
{
    [Header("Stratum Icon")]
    [SerializeField] private Texture2D somato;
    [SerializeField] private Texture2D onero;
    [SerializeField] private Texture2D aether;

    [Header("Temp Icons")]
    [SerializeField] private Texture2D somato_passive;
    [SerializeField] private Texture2D somato_active;
    [SerializeField] private Texture2D onero_active;
    [SerializeField] private Texture2D onero_passive;
    [SerializeField] private Texture2D aether_passive;
    [SerializeField] private Texture2D aether_active;

    [Header("References")]
    [SerializeField] private Image stratum_indicator;
    [SerializeField] private Image stratum_passive_icon;
    [SerializeField] private Image stratum_active_icon;

    public void OnStratumSwitched(CharacterInstance character, Stratum entry_stratum, Stratum departing_stratum)
    {
        var indicator_lookup = new Dictionary<Stratum, Texture2D>();
        indicator_lookup[Stratum.ONERO] = onero;
        indicator_lookup[Stratum.SOMATO] = somato;
        indicator_lookup[Stratum.AETHER] = aether;

        var icon_lookup = new Dictionary<Stratum, List<Texture2D>>();
        icon_lookup[Stratum.ONERO] = new(){ onero_passive, onero_active };
        icon_lookup[Stratum.SOMATO] = new() { somato_passive, somato_active };
        icon_lookup[Stratum.AETHER] = new() { aether_passive, aether_active };


        var tex = indicator_lookup[entry_stratum];
        stratum_indicator.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        var stratum_passive = icon_lookup[entry_stratum][0];
        var stratum_active = icon_lookup[entry_stratum][1];

        stratum_passive_icon.sprite = Sprite.Create(stratum_passive, new Rect(0, 0, stratum_passive.width, stratum_passive.height), Vector2.one * 0.5f);
        stratum_active_icon.sprite = Sprite.Create(stratum_active, new Rect(0, 0, stratum_active.width, stratum_active.height), Vector2.one * 0.5f);
    }
}

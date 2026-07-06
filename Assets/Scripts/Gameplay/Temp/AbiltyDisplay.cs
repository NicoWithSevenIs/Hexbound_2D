using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AbiltyDisplay : MonoBehaviour, IOnPathSwitched
{
    [Header("Path Icon")]
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
    [SerializeField] private Image path_indicator;
    [SerializeField] private Image path_passive_icon;
    [SerializeField] private Image path_active_icon;

    public void OnPathSwitched(CharacterInstance character, Path entry_path, Path departing_path)
    {
        var indicator_lookup = new Dictionary<Path, Texture2D>();
        indicator_lookup[Path.ONERO] = onero;
        indicator_lookup[Path.SOMATO] = somato;
        indicator_lookup[Path.AETHER] = aether;

        var icon_lookup = new Dictionary<Path, List<Texture2D>>();
        icon_lookup[Path.ONERO] = new(){ onero_passive, onero_active };
        icon_lookup[Path.SOMATO] = new() { somato_passive, somato_active };
        icon_lookup[Path.AETHER] = new() { aether_passive, aether_active };


        var tex = indicator_lookup[entry_path];
        path_indicator.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        var path_passive = icon_lookup[entry_path][0];
        var path_active = icon_lookup[entry_path][1];

        path_passive_icon.sprite = Sprite.Create(path_passive, new Rect(0, 0, path_passive.width, path_passive.height), Vector2.one * 0.5f);
        path_active_icon.sprite = Sprite.Create(path_active, new Rect(0, 0, path_active.width, path_active.height), Vector2.one * 0.5f);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PathDisplay : MonoBehaviour, IOnPathSwitched, IOnCharacterSwitched
{
 
    [Serializable]
    public class Highlightable
    {
        public GameObject Subject;
        public Outline PrimaryOutline;
        public Outline SecondaryOutline;

        public void SetPrimaryActive()
        {
            PrimaryOutline.enabled = true;
            SecondaryOutline.enabled = false;
        }
        public void SetSecondaryActive()
        {
            SecondaryOutline.enabled = true;
            PrimaryOutline.enabled = false;
        }
        public void DisableOutlines()
        {
            PrimaryOutline.enabled = false;
            SecondaryOutline.enabled = false;
        }

    }

    [SerializeField] private Highlightable Somato;
    [SerializeField] private Highlightable Onero;
    [SerializeField] private Highlightable Aether;

    private Dictionary<Path , Highlightable> Highlights = new();
    private void Awake()
    {
        Highlights[Path.SOMATO] = Somato;
        Highlights[Path.ONERO] = Onero;
        Highlights[Path.AETHER] = Aether;

        foreach(var (path, highlightable) in Highlights)
        {
            highlightable.DisableOutlines();
        }
    }

    public void OnPathSwitched(CharacterInstance character, Path entry_path, Path departing_path)
    {
        foreach (var path in Paths.Values)
        {
            var highlight = Highlights[path];

            if (path == entry_path)
            {
                highlight.SetPrimaryActive();
                continue;
            }

            if (path == departing_path)
            {
                highlight.SetSecondaryActive();
                continue;
            }

            highlight.DisableOutlines();
        }
    }

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        foreach (var path in Paths.Values)
        {
            var highlight = Highlights[path];

            if (path == entering.CurrentPath)
            {
                highlight.SetPrimaryActive();
                continue;
            }

            highlight.DisableOutlines();
        }
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StratumDisplay : MonoBehaviour, IOnStratumSwitched, IOnCharacterSwitched
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

    private Dictionary<Stratum , Highlightable> Highlights = new();
    private void Awake()
    {
        Highlights[Stratum.SOMATO] = Somato;
        Highlights[Stratum.ONERO] = Onero;
        Highlights[Stratum.AETHER] = Aether;

        foreach(var (stratum, highlightable) in Highlights)
        {
            highlightable.DisableOutlines();
        }
    }

    public void OnStratumSwitched(CharacterInstance character, Stratum entry_stratum, Stratum departing_stratum)
    {
        foreach (var stratum in Strata.Values)
        {
            var highlight = Highlights[stratum];

            if (stratum == entry_stratum)
            {
                highlight.SetPrimaryActive();
                continue;
            }

            if (stratum == departing_stratum)
            {
                highlight.SetSecondaryActive();
                continue;
            }

            highlight.DisableOutlines();
        }
    }

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        foreach (var stratum in Strata.Values)
        {
            var highlight = Highlights[stratum];

            if (stratum == entering.CurrentStratum)
            {
                highlight.SetPrimaryActive();
                continue;
            }

            highlight.DisableOutlines();
        }
    }

}

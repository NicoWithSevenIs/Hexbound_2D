using TMPro;
using UnityEngine;
using Hexbound.Stats;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_StatsPage : MonoBehaviour, IOnCharacterLoaded, IOnCharacterSwitched
{
    #region UI NAV

    [Serializable]
    private class ButtonItem
    {
        public string name;
        public Button button;
    }

    [Header("UI Navigation")]
    [SerializeField] private Vector2 slide_threshold;
    [SerializeField] private float slide_speed;


    [Header("References")]
    [SerializeField] private TextMeshProUGUI character_name;
    [SerializeField] private List<ButtonItem> buttons_list;
    [SerializeField] private TextMeshProUGUI stat_context;

    [Header("Debug")]
    [SerializeField] private string mode = "";
    [SerializeField] private bool is_sliding = false;

    private RectTransform rect;

    public void ToggleFrame(string name)
    {    
        if (is_sliding) return;
        mode = mode != name ? name : "";
        
        is_sliding = true;
    }

    #endregion

    public void OnStatsUpdating(Character_Stats base_stats, Character_Stats build_stats, Character_Stats current_stats)
    {
        stat_context.text = $"{mode} Stats";
        switch (mode)
        {
            case "Base": DisplayStats(base_stats); break;
            case "Build": DisplayStats(build_stats); break; ;
            case "Battle": DisplayStatsRatio(current_stats, build_stats); break;
        }
    }

    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        character_name.text = character1.CharacterData.unit_name;
   
    }
    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        character_name.text = entering.CharacterData.unit_name;
    }

    #region Lifecycle Methods

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!is_sliding) return;

        var pos = rect.anchoredPosition;
        pos.x = string.IsNullOrEmpty(mode) ? slide_threshold.x : slide_threshold.y;

        rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, pos, Time.deltaTime * slide_speed);
        is_sliding = rect.anchoredPosition.x != slide_threshold.x && rect.anchoredPosition.x != slide_threshold.y;
    }
    #endregion

    #region Stat Display
    [Space]
    [Header("Basic Stat Display")]
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI atk;
    [SerializeField] private TextMeshProUGUI def;
    [SerializeField] private TextMeshProUGUI atk_spd;
    [SerializeField] private TextMeshProUGUI mov_spd;

    [Header("Advanced Stat Display")]
    [SerializeField] private TextMeshProUGUI dash_dist;
    [SerializeField] private TextMeshProUGUI spa;
    [SerializeField] private TextMeshProUGUI qsm;
    [SerializeField] private TextMeshProUGUI crit_rate;
    [SerializeField] private TextMeshProUGUI crit_dmg;
    [SerializeField] private TextMeshProUGUI echo_rate;
    [SerializeField] private TextMeshProUGUI echo_count;
    [SerializeField] private TextMeshProUGUI echo_dmg;
    [SerializeField] private TextMeshProUGUI flat_pen;
    [SerializeField] private TextMeshProUGUI percent_pen;
    [SerializeField] private TextMeshProUGUI all_dmg_amp;

    private void DisplayStats(Character_Stats snapshot)
    {
        var bss = snapshot.BasicStats;
        var ass = snapshot.AdvancedStats;

        // Basic stats
        hp.text = bss.HP.ToString();
        atk.text = bss.ATK.ToString();
        def.text = bss.DEF.ToString();
        atk_spd.text = bss.ATK_SPD.ToString();
        mov_spd.text = bss.MOV_SPD.ToString();

        // Advanced stats
        dash_dist.text = $"{ass.DASH_DIST * 100}%";      
        spa.text = ass.SPA.ToString();
        qsm.text = $"{ass.QSM * 100}%";

        crit_rate.text = $"{ass.CRIT_RATE * 100}%";
        crit_dmg.text = $"{ass.CRIT_DMG * 100}%";

        echo_rate.text = $"{ass.ECHO_RATE * 100}%";
        echo_count.text = ass.ECHO_COUNT.ToString();
        echo_dmg.text = $"{ass.ECHO_DMG * 100}%";

        flat_pen.text = ass.FLAT_PEN.ToString();
        percent_pen.text = $"{ass.PERCENT_PEN * 100}%";

        all_dmg_amp.text = $"{ass.ALL_DMG_AMP * 100}%";
    }

    private void DisplayStatsRatio(Character_Stats current, Character_Stats total)
    {
        var c_bss = current.BasicStats;
        var c_ass = current.AdvancedStats;
        var t_bss = total.BasicStats;
        var t_ass = total.AdvancedStats;

        // Basic stats
        hp.text = $"{c_bss.HP}/{t_bss.HP}";
        atk.text = $"{c_bss.ATK}/{t_bss.ATK}";
        def.text = $"{c_bss.DEF}/{t_bss.DEF}";
        atk_spd.text = $"{c_bss.ATK_SPD}/{t_bss.ATK_SPD}";
        mov_spd.text = $"{c_bss.MOV_SPD}/{t_bss.MOV_SPD}";

        // Advanced stats
        dash_dist.text = $"{c_ass.DASH_DIST * 100}%/{t_ass.DASH_DIST * 100}%";
        spa.text = $"{c_ass.SPA}/{t_ass.SPA}";
        qsm.text = $"{c_ass.QSM * 100}%/{t_ass.QSM * 100}%";

        crit_rate.text = $"{c_ass.CRIT_RATE * 100}%/{t_ass.CRIT_RATE * 100}%";
        crit_dmg.text = $"{c_ass.CRIT_DMG * 100}%/{t_ass.CRIT_DMG * 100}%";

        echo_rate.text = $"{c_ass.ECHO_RATE * 100}%/{t_ass.ECHO_RATE * 100}%";
        echo_count.text = $"{c_ass.ECHO_COUNT}/{t_ass.ECHO_COUNT}";   
        echo_dmg.text = $"{c_ass.ECHO_DMG * 100}%/{t_ass.ECHO_DMG * 100}%";

        flat_pen.text = $"{c_ass.FLAT_PEN}/{t_ass.FLAT_PEN}";
        percent_pen.text = $"{c_ass.PERCENT_PEN * 100}%/{t_ass.PERCENT_PEN * 100}%";

        all_dmg_amp.text = $"{c_ass.ALL_DMG_AMP * 100}%/{t_ass.ALL_DMG_AMP * 100}%";
    }

    #endregion
}

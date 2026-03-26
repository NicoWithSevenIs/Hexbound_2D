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

    public void OnStatsUpdating(Stats base_stats, Stats build_stats, Stats current_stats)
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

    private void DisplayStats(Stats s)
    {
        // Basic stats
        hp.text = $"{s[StatType.HP]}";
        atk.text = $"{s[StatType.ATK]}";
        def.text = $"{s[StatType.DEF]}";
        atk_spd.text = $"{s[StatType.ATK_SPEED]}";
        mov_spd.text = $"{s[StatType.MOVE_SPEED]}";

        // Advanced stats
        dash_dist.text = $"{s[StatType.DASH_FORCE] * 100:0.00}%";
        spa.text = $"{s[StatType.SUSTAINED_PATH_AMPLIFICATION]}";
        qsm.text = $"{s[StatType.QUICK_SWAP_MULTIPLIER] * 100:0.00}%";

        crit_rate.text = $"{s[StatType.CRIT_RATE] * 100:0.00}%";
        crit_dmg.text = $"{s[StatType.CRIT_DMG] * 100:0.00}%";

        echo_rate.text = $"{s[StatType.ECHO_RATE] * 100:0.00}%";
        echo_count.text = $"{s[StatType.ECHO_COUNT]}";
        echo_dmg.text = $"{s[StatType.ECHO_DMG] * 100:0.00}%";

        flat_pen.text = $"{s[StatType.FLAT_PEN]}";
        percent_pen.text = $"{s[StatType.PERCENT_PEN] * 100:0.00}%";

        all_dmg_amp.text = $"{s[StatType.ALL_DMG_AMP] * 100:0.00}%";
    }

    private void DisplayStatsRatio(Stats c, Stats t)
    {
        // Basic stats
        hp.text = $"{c[StatType.HP]}/{t[StatType.HP]}";
        atk.text = $"{c[StatType.ATK]}/{t[StatType.ATK]}";
        def.text = $"{c[StatType.DEF]}/{t[StatType.DEF]}";
        atk_spd.text = $"{c[StatType.ATK_SPEED]}/{t[StatType.ATK_SPEED]}";
        mov_spd.text = $"{c[StatType.MOVE_SPEED]}/{t[StatType.MOVE_SPEED]}";

        // Advanced stats
        dash_dist.text = $"{c[StatType.DASH_FORCE] * 100:0.00}/{t[StatType.DASH_FORCE] * 100:0.00}%";
        spa.text = $"{c[StatType.SUSTAINED_PATH_AMPLIFICATION]}/{t[StatType.SUSTAINED_PATH_AMPLIFICATION]}";
        qsm.text = $"{c[StatType.QUICK_SWAP_MULTIPLIER] * 100:0.00}/{t[StatType.QUICK_SWAP_MULTIPLIER] * 100:0.00}%";

        crit_rate.text = $"{c[StatType.CRIT_RATE] * 100:0.00}/{t[StatType.CRIT_RATE] * 100:0.00}%";
        crit_dmg.text = $"{c[StatType.CRIT_DMG] * 100:0.00}/{t[StatType.CRIT_DMG] * 100:0.00}%";

        echo_rate.text = $"{c[StatType.ECHO_RATE] * 100}/{t[StatType.ECHO_RATE] * 100:0.00}%";
        echo_count.text = $"{c[StatType.ECHO_COUNT]}/{t[StatType.ECHO_COUNT]}";
        echo_dmg.text = $"{c[StatType.ECHO_DMG] * 100:0.00}/{t[StatType.ECHO_DMG] * 100}%";

        flat_pen.text = $"{c[StatType.FLAT_PEN]}/{t[StatType.FLAT_PEN]}";
        percent_pen.text = $"{c[StatType.PERCENT_PEN] * 100:0.00}/{t[StatType.PERCENT_PEN] * 100:0.00}%";

        all_dmg_amp.text = $"{c[StatType.ALL_DMG_AMP] * 100:0.00}/{t[StatType.ALL_DMG_AMP] * 100:0.00}%";
    }
    #endregion
}

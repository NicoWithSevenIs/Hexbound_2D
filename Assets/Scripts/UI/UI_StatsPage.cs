using TMPro;
using UnityEngine;
using Hexbound.Stats;

public class UI_StatsPage : MonoBehaviour
{
    #region UI NAV
    [Header("UI Navigation")]
    [SerializeField] private Vector2 slide_threshold;
    [SerializeField] private float slide_speed;
    private bool is_open = false;
    private bool is_sliding = false;

    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!is_sliding) return;

        var pos = rect.anchoredPosition;
        pos.x = !is_open ? slide_threshold.x : slide_threshold.y;


        rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, pos, Time.deltaTime * slide_speed);

        if (rect.anchoredPosition.x == slide_threshold.x || rect.anchoredPosition.x == slide_threshold.y)
        {
            is_sliding = false;
        }
            
    }

    public void ToggleFrame()
    {    
        if (is_sliding) return;
        is_open = !is_open;
        is_sliding = true;
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

    public void UpdateStatDisplay(CharacterStat_Snapshot snapshot)
    {

        var bss = snapshot.basic_snapshot;
        var ass = snapshot.advanced_snapshot;

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
    #endregion
}

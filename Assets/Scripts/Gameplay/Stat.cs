using Hexbound.Stats;
using System;
using UnityEngine;

namespace Hexbound.Stats
{
    [Serializable]
    public class Basic_Stat
    {
        public float HP = 0;
        public float ATK = 0;
        public float DEF = 0;
        public float ATK_SPD = 0;
        public float MOV_SPD = 0;
    }

    [Serializable]
    public class Advanced_Stat
    {
        /*Dash Distance - Percentage amplification of the fixed base dash distance*/
        public float DASH_DIST = 1f;

        /* Quick Swap Multiplier - After switching paths, refresh this 
        bonus. Staying longer for more than 1.15 seconds in one path will 
        cause this Multiplier to linearly decay to 0% over 3 seconds. */

        [Tooltip("Quick Swap Multiplier")]
        public float QSM = 1f;

        /* Sustained Path Amplifier - After switching paths, reset this 
        bonus to 0%. Staying longer for more than 1.15 seconds in one path 
        will cause this Multiplier to linearly ramp to 100% of its base value
        over 3 seconds. */

        [Tooltip("Sustained Path Amplifier")]
        public float SPA = 1f;

        /*PEN Stats - Ignores a portion of the target's DEF during damage calculation.
                -> FLAT_PEN - flat def ignore, applied after Percent Def Ignore.
                -> PERCENT_PEN - pen pineapple apple pen*/
        public uint FLAT_PEN = 0;
        public float PERCENT_PEN = 0f;


        /*Crit Stats - Chance to multiply damage by the Crit DMG Multiplier*/
        public float CRIT_RATE = 0.05f;
        public float CRIT_DMG = 1.6f;


        /*Echo Stats - Chance to repeat a portion of the DMG dealt as Additional 
         DMG multiple times equal to the Echo Count*/
        public float ECHO_RATE = 0.05f;
        public uint ECHO_COUNT = 3;
        public float ECHO_DMG = 0.2f;

        /*DMG AMP - Amplifies final damage dealt*/
        public float ALL_DMG_AMP = 1;
    }

    [Serializable]
    public class Character_Stats
    {
        public Basic_Stat BasicStats;
        public Advanced_Stat AdvancedStats;
    }
   
    public static class Utilities
    {
        public static CharacterStat_Snapshot Snapshot(Character_Stats c_stats)
        {
            CharacterStat_Snapshot snapshot = new();
            snapshot.basic_snapshot = new BasicStat_Snapshot(c_stats.BasicStats);
            snapshot.advanced_snapshot = new AdvancedStat_Snapshot(c_stats.AdvancedStats);
            return snapshot;
        }
    }

    #region Stat Snapshot

    public class CharacterStat_Snapshot
    {
        public BasicStat_Snapshot basic_snapshot;
        public AdvancedStat_Snapshot advanced_snapshot;
    }

    public struct BasicStat_Snapshot
    {
        public float HP;
        public float ATK;
        public float DEF;
        public float ATK_SPD;
        public float MOV_SPD;

        public BasicStat_Snapshot(Basic_Stat stat)
        {
            HP = stat.HP;
            ATK = stat.ATK;
            DEF = stat.DEF;
            ATK_SPD = stat.ATK_SPD;
            MOV_SPD = stat.MOV_SPD;
        }
    }

    public struct AdvancedStat_Snapshot
    {
        public float DASH_DIST;
        public float QSM;
        public float SPA;

        public uint FLAT_PEN;
        public float PERCENT_PEN;

        public float CRIT_RATE;
        public float CRIT_DMG;

        public float ECHO_RATE;
        public uint ECHO_COUNT;
        public float ECHO_DMG;

        public float ALL_DMG_AMP;

        public AdvancedStat_Snapshot(Advanced_Stat stat)
        {
            DASH_DIST = stat.DASH_DIST;

            QSM = stat.QSM;
            SPA = stat.SPA;

            FLAT_PEN = stat.FLAT_PEN;
            PERCENT_PEN = stat.PERCENT_PEN;

            CRIT_RATE = stat.CRIT_RATE;
            CRIT_DMG = stat.CRIT_DMG;

            ECHO_RATE = stat.ECHO_RATE;
            ECHO_COUNT = stat.ECHO_COUNT;
            ECHO_DMG = stat.ECHO_DMG;

            ALL_DMG_AMP = stat.ALL_DMG_AMP;
        }
    }
    #endregion

}


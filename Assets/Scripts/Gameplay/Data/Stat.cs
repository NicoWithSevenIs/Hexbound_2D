using Hexbound.Stats;
using System;
using UnityEngine;

namespace Hexbound.Stats
{
    [Serializable]
    public partial class Basic_Stat
    {
        public float HP = 0;
        public float ATK = 0;
        public float DEF = 0;
        public float ATK_SPD = 0;
        public float MOV_SPD = 0;
    }

    [Serializable]
    public partial class Advanced_Stat
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
        public int FLAT_PEN = 0;
        public float PERCENT_PEN = 0f;


        /*Crit Stats - Chance to multiply damage by the Crit DMG Multiplier*/
        public float CRIT_RATE = 0.05f;
        public float CRIT_DMG = 1.6f;


        /*Echo Stats - Chance to repeat a portion of the DMG dealt as Additional 
         DMG multiple times equal to the Echo Count*/
        public float ECHO_RATE = 0.05f;
        public int ECHO_COUNT = 3;
        public float ECHO_DMG = 0.2f;

        /*DMG AMP - Amplifies final damage dealt*/
        public float ALL_DMG_AMP = 1;
    }

    [Serializable]
    public partial class Character_Stats
    {
        public Basic_Stat BasicStats;
        public Advanced_Stat AdvancedStats;
    }

    #region Stat Operators
    public partial class Basic_Stat
    {
        public static Basic_Stat operator +(Basic_Stat a, Basic_Stat b)
        {
            Basic_Stat sum = new();

            sum.HP = a.HP + b.HP;
            sum.ATK = a.ATK + b.ATK;
            sum.DEF = a.DEF + b.DEF;
            sum.ATK_SPD = a.ATK_SPD + b.ATK_SPD;
            sum.MOV_SPD = a.MOV_SPD + b.MOV_SPD;

            return sum;
        }

        public static Basic_Stat operator -(Basic_Stat v)
        {
            Basic_Stat negated = new();

            negated.HP = -v.HP;
            negated.ATK = -v.ATK;
            negated.DEF = -v.DEF;
            negated.ATK_SPD = -v.ATK_SPD;
            negated.MOV_SPD = -v.MOV_SPD;

            return negated;
        }

        public static Basic_Stat operator -(Basic_Stat a, Basic_Stat b)
        {
            return a + -b;
        }

    }

    public partial class Advanced_Stat
    {
        public static Advanced_Stat operator +(Advanced_Stat a, Advanced_Stat b)
        {
            Advanced_Stat sum = new();

            sum.DASH_DIST = a.DASH_DIST + b.DASH_DIST;
            sum.QSM = a.QSM + b.QSM;
            sum.SPA = a.SPA + b.SPA;
            sum.CRIT_RATE = a.CRIT_RATE + b.CRIT_RATE;
            sum.CRIT_DMG = a.CRIT_DMG + b.CRIT_DMG;
            sum.ECHO_RATE = a.ECHO_RATE + b.ECHO_RATE;
            sum.ECHO_COUNT = a.ECHO_COUNT + b.ECHO_COUNT;
            sum.ECHO_DMG = a.ECHO_DMG + b.ECHO_DMG;
            sum.FLAT_PEN = a.FLAT_PEN + b.FLAT_PEN;
            sum.PERCENT_PEN = a.PERCENT_PEN + b.PERCENT_PEN;
            sum.ALL_DMG_AMP = a.ALL_DMG_AMP + b.ALL_DMG_AMP;

            return sum;
        }

        public static Advanced_Stat operator -(Advanced_Stat v)
        {
            Advanced_Stat negated = new();

            negated.DASH_DIST = -v.DASH_DIST;
            negated.QSM = -v.QSM;
            negated.SPA = -v.SPA;
            negated.CRIT_RATE = -v.CRIT_RATE;
            negated.CRIT_DMG = -v.CRIT_DMG;
            negated.ECHO_RATE = -v.ECHO_RATE;
            negated.ECHO_COUNT = -v.ECHO_COUNT;
            negated.ECHO_DMG = -v.ECHO_DMG;
            negated.FLAT_PEN = -v.FLAT_PEN;
            negated.PERCENT_PEN = -v.PERCENT_PEN;
            negated.ALL_DMG_AMP = -v.ALL_DMG_AMP;

            return negated;
        }

        public static Advanced_Stat operator -(Advanced_Stat a, Advanced_Stat b)
        {
            return a + -b;
        }
    }

    public partial class Character_Stats
    {
        public static Character_Stats operator+(Character_Stats a, Character_Stats b)
        {
            Character_Stats sum = new();
            sum.BasicStats = a.BasicStats + b.BasicStats;
            sum.AdvancedStats = a.AdvancedStats + b.AdvancedStats;
            return sum;
        }

        public static Character_Stats operator -(Character_Stats v)
        {
            Character_Stats negated = new();
            negated.BasicStats = -v.BasicStats;
            negated.AdvancedStats = -v.AdvancedStats;
            return negated;
        }

        public static Character_Stats operator - (Character_Stats a, Character_Stats b)
        {
            return a + -b;
        }
    }
    #endregion

    #region Constructors
    public partial class Basic_Stat
    {
        public Basic_Stat(){}

        //copy constructor
        public Basic_Stat(Basic_Stat original)
        {
            HP = original.HP;
            ATK = original.ATK;
            DEF = original.DEF;
            ATK_SPD = original.ATK_SPD;
            MOV_SPD = original.MOV_SPD;
        }
    }

    public partial class Advanced_Stat
    {
        public Advanced_Stat() { }
        public Advanced_Stat(Advanced_Stat original)
        {
            DASH_DIST = original.DASH_DIST;
            SPA = original.SPA;
            QSM = original.QSM;
            CRIT_RATE = original.CRIT_RATE;
            CRIT_DMG = original.CRIT_DMG;
            ECHO_RATE = original.ECHO_RATE;
            ECHO_COUNT = original.ECHO_COUNT;
            ECHO_DMG = original.ECHO_DMG;
            FLAT_PEN = original.FLAT_PEN;
            PERCENT_PEN = original.PERCENT_PEN;
            ALL_DMG_AMP = original.ALL_DMG_AMP;
        }
    }

    public partial class Character_Stats
    {
        public Character_Stats() { }
        public Character_Stats(Character_Stats original){
            BasicStats = new(original.BasicStats);
            AdvancedStats = new(original.AdvancedStats);
        }
    }
    #endregion

    [Serializable]
    public class Hidden_Stat
    {
        public static readonly float FIXED_DASH_COOLDOWN = 1f;

        public int MAX_JUMPS;

        public float JUMP_FORCE;
        public float DASH_FORCE;
    }

    public enum STAT
    {
        HP,
        ATK,
        DEF,
        MOVE_SPEED,
        ATK_SPEED,
        DASH_DIST,
        SUSTAINED_PATH_AMPLIFICATION,
        QUICK_SWAP_MULTIPLIER,
        CRIT_RATE,
        CRIT_DMG,
        ECHO_RATE,
        ECHO_COUNT,
        ECHO_DMG,
        FLAT_PEN,
        PERCENT_PEN,
        ALL_DMG_AMP,
    }
}


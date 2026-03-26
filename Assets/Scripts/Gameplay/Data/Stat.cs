using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Hexbound.Stats
{

    public enum StatType
    {
        //Basic
        HP,
        ATK,
        DEF,
        MOVE_SPEED,
        ATK_SPEED,

        //Mobility
        MAX_JUMPS,
        JUMP_FORCE,
        DASH_FORCE,

        // Damage Calcs

        /* Sustained Path Amplifier - After switching paths, reset this 
        bonus to 0%. Staying longer for more than 1.15 seconds in one path 
        will cause this Multiplier to linearly ramp to 100% of its base value
        over 3 seconds. */
        SUSTAINED_PATH_AMPLIFICATION,

        /* Quick Swap Multiplier - After switching paths, refresh this 
            bonus. Staying longer for more than 1.15 seconds in one path will 
            cause this Multiplier to linearly decay to 0% over 3 seconds. */
        QUICK_SWAP_MULTIPLIER,

        /*Crit Stats - Chance to multiply damage by the Crit DMG Multiplier*/
        CRIT_RATE,
        CRIT_DMG,

        /*Echo Stats - Chance to repeat a portion of the DMG dealt as Additional 
            DMG multiple times equal to the Echo Count*/
        ECHO_RATE,
        ECHO_COUNT,
        ECHO_DMG,

        /*PEN Stats - Ignores a portion of the target's DEF during damage calculation.
                -> FLAT_PEN - flat def ignore, applied after Percent Def Ignore.
                -> PERCENT_PEN - pen pineapple apple pen*/
        FLAT_PEN,
        PERCENT_PEN,

        /*DMG AMP - Amplifies final damage dealt*/
        ALL_DMG_AMP,

        //Context DMG Amplifiers
        GROUNDED_DMG_BONUS,
        AERIAL_DMG_BONUS,

        //Action DMG Amplifiers
        BASIC_ATK_DMG_BONUS,
        HEAVY_ATK_DMG,
        PLUNGE_DMG_BONUS,

        GROUNDED_BASIC_DMG_BONUS,
        GROUNDED_HEAVY_DMG_BONUS,
        AERIAL_BASIC_DMG_BONUS,
        AERIAL_HEAVY_DMG_BONUS,
    }

    [Serializable]
    public class Stats
    {

        [SerializeField]
        [SerializedDictionary("Stat", "Value")]
        private SerializedDictionary<StatType, float> stats = new();

        public float this[StatType stat]
        {
            get => stats.ContainsKey(stat) ? stats[stat] : 0;
            set 
            {
                if (stats.ContainsKey(stat))
                {
                    stats[stat] = value;
                }     
            }
        }


        #region Constructors
        public Stats() { }
        public Stats(Stats other)
        {
            this.stats = new(other.stats);
        }




        #endregion Constructors

        #region Stat Operators

        public static Stats operator +(Stats a, Stats b)
        {
            Stats sum = new();
            var stats = Enum.GetValues(typeof(StatType));
            foreach (StatType stat in stats)
            {
                if (a.stats.ContainsKey(stat) && b.stats.ContainsKey(stat))
                {
                    sum[stat] = a[stat] + b[stat];
                }
            }
            return sum;
        }
        public static Stats operator -(Stats a)
        {
            Stats negated = new();
            var stats = Enum.GetValues(typeof(StatType));
            foreach (StatType stat in stats)
            {
                negated[stat] = -a[stat];
            }
            return negated;
        }

        public static Stats operator -(Stats a, Stats b)
        {
            return a + -b;
        }

        
        #endregion
    }




}
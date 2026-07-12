using System;
using UnityEngine;

public enum Stratum
{
    SOMATO,
    ONERO,
    AETHER
}

public static class Strata
{
    public static readonly Stratum[] Values = (Stratum[])Enum.GetValues(typeof(Stratum));
}

using System;
using UnityEngine;

public enum Path
{
    SOMATO,
    ONERO,
    AETHER
}

public static class Paths
{
    public static readonly Path[] Values = (Path[])Enum.GetValues(typeof(Path));
}

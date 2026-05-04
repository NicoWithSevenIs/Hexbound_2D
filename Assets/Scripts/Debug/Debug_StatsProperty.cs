using Hexbound.Stats;
using UnityEngine;

public class Debug_StatsProperty : MonoBehaviour
{
    [SerializeField] private Character stats;
    [SerializeField] private Stats stat;


    private void Start()
    {
        stat = stats.base_stats;
    }


}

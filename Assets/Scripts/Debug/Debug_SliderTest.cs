using UnityEngine;
using UnityEngine.UI;

public class Debug_SliderTest : MonoBehaviour
{

    [SerializeField] private Image qsm;
    [SerializeField] private Image spa;

    [SerializeField]
    [Range(0f, 1f)]
    private float fill_amount = 0.5f;

    private void OnValidate()
    {
        if(qsm != null && spa != null)
        {
            qsm.fillAmount = fill_amount;
            spa.fillAmount = 1 - fill_amount;
        }
    }
}

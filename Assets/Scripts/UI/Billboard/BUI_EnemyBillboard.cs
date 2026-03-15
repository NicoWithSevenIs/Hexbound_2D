using UnityEngine;
using UnityEngine.UI;

public class BUI_EnemyBillboard : MonoBehaviour
{
    [SerializeField] private Canvas billboard_canvas;
    [SerializeField] private Image health_bar;

    public void OnDamageTaken(float dmg, float curr_hp, float base_hp)
    {
        float ratio = curr_hp/ base_hp;
        billboard_canvas.gameObject.SetActive(ratio < 1);
        if (billboard_canvas.gameObject.activeSelf)
        {
            health_bar.fillAmount = ratio;
        }
    }

}

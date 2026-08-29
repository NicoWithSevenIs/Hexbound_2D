using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChainLightning : MonoBehaviour
{
    private Timer timer;
    private LineRenderer line_renderer;
    private List<Vector3> chain_pos_list;

    private void Start()
    {
        line_renderer = GetComponent<LineRenderer>();
        void OnElapse()
        {
            GameObject.Destroy(gameObject);
        }
        timer = new Timer(0.6f, OnElapse, false);
    }

    private void Update()
    {
        timer.Tick();
        line_renderer.positionCount = chain_pos_list.Count;
        line_renderer.SetPositions(chain_pos_list.ToArray());
    }

    public void Initialize(List<Vector3> chain)
    {
        chain_pos_list = chain;
    }
}

using System;
using UnityEngine;

public class IntervalAction
{
    private Action action;
    private float time;

    private float timer = 0f;
    public IntervalAction(float time, Action callback)
    {
        this.time = time;
        action = callback;
    }

    public void Tick()
    {
        timer += Time.deltaTime;

        if(timer > time)
        {
            action();
            timer = 0f;
        }
    }
}

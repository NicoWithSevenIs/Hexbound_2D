using System;
using UnityEngine;

public class Timer
{
    private Action on_elapsed_callback = null;
    private Action on_start_callback = null;

    private float time = Mathf.Infinity;
    private float timer = 0f;

    private bool is_running = true;
    private bool loops = false;

    public Timer(float time, Action on_elapsed_callback, bool loops = true)
    {
        this.time = time;
        this.on_elapsed_callback = on_elapsed_callback;
        this.loops = loops;
    }

    public Timer(float time)
    {
        is_running = false;
    }

    public void Tick()
    {
        if (!is_running)
            return;

        timer += Time.deltaTime;

        if(timer >= time)
        {
            on_elapsed_callback?.Invoke();
            timer = 0f;
            if (!loops)
            {
              is_running = false;
            }
        }
    }

    public void Run()
    {
        if (!is_running && timer == 0f && time != Mathf.Infinity)
        {
            on_start_callback?.Invoke();
            is_running = true;
        }
    }

    public void Pause()
    {
        if (is_running)
        {
            is_running = false;
        }
    }

    public void Resume()
    {
        if (!is_running && timer > 0f)
        {
            is_running = true;
        }
    }

    public void Stop()
    {
        if (is_running)
        {
            is_running = false;
            timer = 0f;
        }
    }


    #region getters & setters
    public bool IsRunning { get => is_running; }
    public bool Loops {  get => loops; set => loops = value; }
    public float MaxTime { set => time = value; }
    public Action OnElapsedCallback { set => on_elapsed_callback = value; } 
    public Action OnStartCallback { set => on_start_callback = value;}
    public float TimeElapsed { get => timer; }
    #endregion
}

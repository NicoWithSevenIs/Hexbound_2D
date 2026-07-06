using System;
using UnityEngine;

public class Propagator : MonoBehaviour
{
    public Action callback;

    public void RunCallback()
    {
        callback?.Invoke();
        //callback = null;
    }

}

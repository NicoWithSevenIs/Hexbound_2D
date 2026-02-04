using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T: class
{
    private static T instance = null;
    public static T Instance { get => instance ?? (instance = Activator.CreateInstance(typeof(T)) as T); }
}

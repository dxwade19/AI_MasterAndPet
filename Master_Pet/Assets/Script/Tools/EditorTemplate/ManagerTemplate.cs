using System;
using UnityEngine;


public abstract class ManagerTemplate<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = default(T);
    public static T Instance => instance;


    protected virtual void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this as T;
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameObjectExtension
{
    public static T TryAddComponent<T>(this GameObject _object) where T : Component
    {
        T _component = _object.GetComponent<T>();
        if (!_component)
            _component = _object.AddComponent<T>();
        return _component;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static void ToWireSphere(this Vector3 _vector3, Color _color, float _size =1)
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(_vector3, _size);
        Gizmos.color = Color.white;
    }


    public static void ToWireCube(this Vector3 _vector3, Color _color, Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.DrawWireCube(_vector3, _size);
        Gizmos.color = Color.white;
    }

    public static void ToCube(this Vector3 _vector3, Color _color, Vector3 _size)
    {
        Gizmos.color = _color;
        Gizmos.DrawCube(_vector3, _size);
        Gizmos.color = Color.white;
    }

    public static void ToLine(this Vector3 _vector3, Vector3 _target, Color _color)
    {
        Gizmos.color = _color;
        Gizmos.DrawLine(_vector3, _target);
        Gizmos.color = Color.white;
    }

    public static void ToLines(this Vector3[] _vector3, Color _color, bool _loop = false)
    {
        Gizmos.color = _color;
        
        for (int i = 0; i < _vector3.Length; i++)
        {
            if (i < _vector3.Length - 1) _vector3[i].ToLine(_vector3[i + 1], _color);
            else if (_loop && i == _vector3.Length - 1) _vector3[i].ToLine(_vector3[0], _color);
        }
    }

    public static void ToCircle(this Vector3 _vector3, float _radius ,Color _color) 
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = _color;
        UnityEditor.Handles.CircleHandleCap(0, _vector3, Quaternion.Euler(90, 0, 0), _radius, EventType.Repaint);
#endif
    }

}

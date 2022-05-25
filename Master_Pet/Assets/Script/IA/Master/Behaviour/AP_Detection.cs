using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AP_Detection : MonoBehaviour
{
    public event Action<ITarget> onTargetDetected = null;
    public event Action<ITarget> OnTargetLost = null;

    [SerializeField] AP_Target target = null;
    [SerializeField, Range(0, 20)] int detectionRange = 2;

    public bool IsValid => target;

    public bool IsDetected { get; private set; } = false;
    public bool IsAtRange
    {
        get
        {
            if (!IsValid) return false;
            return Vector3.Distance(transform.position, target.TargetPosition) < detectionRange;
        }
    }
    public int DetectionRange => detectionRange;

    private void Awake()
    {
        onTargetDetected += (point) => IsDetected = true;
        OnTargetLost += (target) => IsDetected = false;
    }

    private void OnDestroy()
    {
        onTargetDetected = null;
        OnTargetLost = null;
    }

    private void OnDrawGizmos()
    {
        if (IsDetected && IsValid)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.TargetPosition);
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void UpdateDetection()
    {
        if (!IsValid) return;
        bool inRange = IsAtRange;
        if (inRange && !IsDetected)
            onTargetDetected?.Invoke(target.GetComponent<ITarget>());
        else if(!inRange && IsDetected)
            OnTargetLost?.Invoke(target.GetComponent<ITarget>());
    }
}

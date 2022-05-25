using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AP_Movements : MonoBehaviour
{
    public event Action OnPositionReached = null;
    public event Action OnMoving = null;

    [SerializeField] Vector3 targetPosition = Vector3.zero;
    [SerializeField, Range(.1f, 10)] float atPosRange = .1f;
    [SerializeField, Range(.5f, 5)] float speed = 2;

    public Vector3 CurrentPoistion => transform.position;

    public bool IsAtPosition => Vector3.Distance(CurrentPoistion, targetPosition) < atPosRange;

    public void SetTarget(Vector3 _target) => targetPosition = _target;
    public void SetTarget(float _x, float _y, float _z) => targetPosition = new Vector3(_x, _y, _z);

    public void MoveTo()
    {
        if (IsAtPosition)
        {
            OnPositionReached?.Invoke();
            return;
        }
        OnMoving?.Invoke();
        transform.position = Vector3.MoveTowards(CurrentPoistion, targetPosition, Time.deltaTime * speed);
        //todo change smoothLookAt
        transform.LookAt(targetPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, .1f);
        Gizmos.DrawLine(transform.position, targetPosition);
    }

    private void OnDestroy()
    {
        OnPositionReached = null;
        OnMoving = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CS_SightData
{
    float currentLenght = 0, maxLenght = 0;
    Transform transformOrigin = null;
    int sightAngle = 0;
    bool isObstacle = false;

    public ITarget Target { get; private set; } = null;
    public bool TargetDetected { get; private set; } = false;
    public bool IsValid => transformOrigin;

    public CS_SightData(int _angle, Transform _origin, float _distance)
    {
        sightAngle = _angle;
        transformOrigin = _origin;
        maxLenght = _distance;
    }

    Vector3 GetSightDirection()
    {
        if (!IsValid) return Vector3.zero;
        return Quaternion.AngleAxis(sightAngle, transformOrigin.up) * transformOrigin.forward;
    }

    public void Detection(LayerMask _obstacleLayer, LayerMask _targetLayer)
    {
        if (!IsValid) return;
        isObstacle = Physics.Raycast(transformOrigin.position, GetSightDirection(), out RaycastHit _hitObstacle, maxLenght, _obstacleLayer);
        if (isObstacle)
            currentLenght = _hitObstacle.distance;
        bool _isTarget= Physics.Raycast(transformOrigin.position, GetSightDirection(), out RaycastHit _hitTarget, GetLenght(), _targetLayer);

        ITarget _target = _isTarget ? _hitTarget.collider?.GetComponent<ITarget>() : null;
        TargetDetected = _target != null;
        Target = _target;
    }

    float GetLenght() => isObstacle ? currentLenght : maxLenght;

    public void DrawDebug()
    {
        if (!IsValid) return;
        Debug.DrawRay(transformOrigin.position, GetSightDirection() * GetLenght(), Color.Lerp(Color.red, Color.green, GetLenght() / maxLenght));
    }
}

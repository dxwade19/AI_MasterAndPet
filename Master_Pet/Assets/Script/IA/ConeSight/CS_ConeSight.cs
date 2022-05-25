using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CS_ConeSight : MonoBehaviour
{
    public event Action<ITarget> OnTargetDetected = null;
    public event Action<Vector3> OnTargetLost = null;

    bool targetInSight = false;

    public event Action OnUpdateSightDebug = null;
    public event Action OnUpdateDelayed = null;

    ITarget targetSave = null;
    [SerializeField, Range(0, 360)] int sightAngle = 90;
    [SerializeField, Range(10, 100)] float sightDistance = 10;
    [SerializeField, Range(.1f, 1)] float updateTickRate = .1f;
    CS_SightData[] sightData = null;
    [SerializeField] LayerMask obstacleLayer = 0, targetlayer = 0;
    private void Awake()
    {
        OnTargetDetected += (target) => targetInSight = true;
        OnTargetLost += (target) => targetInSight = false;
    }

    private void Start()
    {
        GenerateSight();
        InvokeRepeating("UpdateConeSightDelayed", 0, updateTickRate);
    }

    void GenerateSight()
    {
        OnUpdateSightDebug = null;
        OnUpdateDelayed = null;
        sightData = new CS_SightData[sightAngle];
        int _index = 0;
        for (int i = -sightAngle / 2; i < sightAngle / 2; i++)
        {
            CS_SightData _sight = new CS_SightData(i, transform, sightDistance);
            OnUpdateSightDebug += _sight.DrawDebug;
            OnUpdateDelayed += () => _sight.Detection(obstacleLayer, targetlayer);
            sightData[_index] = _sight;
            _index++;
        }
        OnUpdateDelayed += MyCheckSightInfo;
    }

    void UpdateConeSightDelayed()
    {
        OnUpdateDelayed?.Invoke();
    }

    void MyCheckSightInfo()
    {
        bool _playerInSight = sightData.Any(_sightData => _sightData.TargetDetected);
        if (_playerInSight && !targetInSight)
        {
            targetSave = sightData.FirstOrDefault(_sightData => _sightData.TargetDetected).Target;
            OnTargetDetected?.Invoke(targetSave);
        }
        else if (!_playerInSight && targetInSight)
        {
            OnTargetLost?.Invoke(targetSave.TargetPosition);
            targetSave = null;
        }
    }

    void CheckSightInfo()
    {
        targetInSight = sightData.Any(_sightData => _sightData.TargetDetected);
        if (!targetInSight && targetSave != null)
        {
            OnTargetLost?.Invoke(targetSave.TargetPosition);
            targetSave = null;
        }

        if (targetInSight && targetSave==null)
        {
            CS_SightData _data = sightData.FirstOrDefault(s => s.TargetDetected);
            targetSave = _data.Target;
            OnTargetDetected?.Invoke(_data.Target);
        }
    }


    private void OnDestroy()
    {
        OnUpdateSightDebug = null;
        OnUpdateDelayed = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = targetInSight ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.up * 2, .2f);
        if (targetInSight && targetSave != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, targetSave.TargetPosition);
        }
    }

    private void Update() => OnUpdateSightDebug?.Invoke();
}

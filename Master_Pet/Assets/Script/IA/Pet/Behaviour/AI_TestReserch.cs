using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI_TestReserch : MonoBehaviour
{
    public event Action OnTrySucess = null;
    public event Action OnTryFailed = null;
    public event Action OnFailedReSearch = null;

    [SerializeField] AP_Detection detection = null;
    [SerializeField] AI_RewardSystem rewardSystem = null;
    [SerializeField] Transform lastPos = null;

    [SerializeField, Header("Detection Stat"), Range(0, 10)] float detectionRange = 4;
    [SerializeField, Range(1, 100)] float failedPourcent = 20;
    [SerializeField] float attempt = 0;
    Vector3 targetToMovePos = Vector3.zero;

    public bool IsValid => lastPos != null && rewardSystem;
    Vector3 targetLastPos => lastPos.position;
   

    void Start()
    {
        OnTryFailed += DefineRandomPoint;
        OnTryFailed += () => attempt++;

        OnTrySucess += () => Debug.Log("find"); //SetBool ("moveToAttack", true) in Brain
        //OnTrySucess += () brain.Pet.PlayerFound(detection.TargetDetection); in state

        DefineRandomPoint();
    }


    void Update() => UpdateDetection();

    

    void UpdateDetection()
    {
        if (detection.IsDetected)
            OnTrySucess?.Invoke();

        else if (IsAtPos()) OnTryFailed?.Invoke();
        UpdateInState();
    }

    void UpdateInState()
    {
        Move(); // Brain.Mouvement.SetTargetMovePos in State To reserch
        detection.UpdateDetection(); // Brain.Detection.UpdateDetection in State To research
    }


    void DefineRandomPoint()
    {
        Vector3 _randomPos = UnityEngine.Random.insideUnitSphere * (detectionRange - 2);
        _randomPos.y = targetLastPos.y;
        targetToMovePos = _randomPos;
    }


    //To Remove
    public bool IsAtPos() => Vector3.Distance(transform.position, targetToMovePos) < 0.1f;
    void Move() => transform.position = Vector3.MoveTowards(transform.position, targetToMovePos, Time.deltaTime * 2);


    void OnDrawGizmos()
    {
        if (!IsValid) return;
        targetLastPos.ToCircle(detectionRange, Color.blue);
        targetToMovePos.ToWireSphere(Color.green, 0.1f);
    }
}

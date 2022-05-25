using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AP_ErikaSearchPlayer : MonoBehaviour
{
    public event Action OnStopSearch = null;
    public event Action OnTryFailed = null;

    [SerializeField, Range(0.1f, 10)] float researchRange = 4;
    [SerializeField, Range(0, 100)] float maxAtempt = 50;
    [SerializeField, Range(0, 100)] float failPct = 20;

    List<float> allPosToCheck = new List<float>();

    public bool Fail => failPct < (atempt / maxAtempt) * 100;
    int failCount = 0;
    int winCount = 0;
    float atempt = 0;

    AI_Pet pet = null;

    Vector3 targetLastPos = Vector3.zero;
    Vector3 targetToMovePos = Vector3.zero;

    public bool IsValid => pet;
    public float WinCount => winCount;
    public float FailCount => failCount;


    private void Start()
    {
        maxAtempt = (int)maxAtempt;
        OnStopSearch += () =>
        {
            failCount++;
            ResetSearch();
        };
    }
    void OnDrawGizmos()
    {
        if (!IsValid) return;
        targetLastPos.ToCircle(researchRange, Color.blue);
        targetToMovePos.ToWireSphere(Color.green, 0.1f);
    }

    private void OnDestroy()
    {
        OnStopSearch = null;
        OnTryFailed = null;
    }

    public void Init()
    {
        if (!IsValid) return;
        pet.MouseBrain.Mouvement.OnPositionReached += UpdateSearch;
        pet.MouseBrain.Detection.OnTargetDetected += (target) =>
        {
            winCount++;
            ResetSearch();
        };
        InitList();
        UpdateSearch();
    }

    void InitList()
    {
        int countPointToCheck = (int)(failPct * maxAtempt / 100);
        int angle = 360 / countPointToCheck;
        for (int i = 0; i < countPointToCheck; i++)
            allPosToCheck.Add(angle * i);
    }

    public void SetTarget(Vector3 _target) => targetLastPos = _target;
    public void SetPet(AI_Pet _pet) => pet = _pet;

    void DefineRandomPoint()
    {
        int random = Random.Range(0, allPosToCheck.Count);
        float randomAngle = allPosToCheck[random];
        float _x = Mathf.Cos(randomAngle) * researchRange;
        float _y = 0;
        float _z = Mathf.Sin(randomAngle) * researchRange;
        targetToMovePos = targetLastPos + new Vector3(_x, _y, _z);
        allPosToCheck.RemoveAt(random);
    }


    void UpdateSearch()
    {
        atempt++;
        if (Fail)
        {
            OnStopSearch?.Invoke();
            return;
        }
        DefineRandomPoint();
        pet.Search(targetToMovePos);
    }

    public void ResetSearch()
    {
        atempt = 0;
        allPosToCheck.Clear();


        pet.MouseBrain.Mouvement.OnPositionReached -= UpdateSearch;
        pet.MouseBrain.Detection.OnTargetDetected -= (target) =>
        {
            winCount++;
            ResetSearch();
        };
    }
}

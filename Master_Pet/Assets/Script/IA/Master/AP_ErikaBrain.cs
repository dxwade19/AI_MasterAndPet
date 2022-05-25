using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AP_ErikaBrain : MonoBehaviour
{
    public event Action OnFinishInit = null;

    [SerializeField] Animator fsm = null;

    [SerializeField] AP_Movements movements = null;
    [SerializeField] CS_ConeSight detection = null;
    [SerializeField] AP_WayPointSystem pattern = null;
    [SerializeField] AP_FightSystem atk = null;
    [SerializeField] AP_MasterPet master = null;
    [SerializeField] AP_ErikaSearchPlayer search = null;

    [SerializeField] string patternParam = "follow_pattern";
    [SerializeField] string atkParam = "atk";
    [SerializeField] string waitParam = "wait";
    [SerializeField] string waitFloatParam = "wait_time";
    [SerializeField] string searchParam = "search";
    [SerializeField] string chaseparam = "chase";
    public string WaitParam => waitParam;
    public string WaitFloatParam => waitFloatParam;
    public AP_Movements Movements => movements;
    public CS_ConeSight Detection => detection;
    public AP_WayPointSystem Pattern => pattern;
    public AP_FightSystem Atk => atk;
    public AP_MasterPet Master => master;
    public AP_ErikaSearchPlayer Search => search;
    public Animator Fsm => fsm;
    public bool IsValid => fsm && movements && detection && pattern && atk && master && search;

    private void Start()
    {
        InitFsm();
    }

    void InitFsm()
    {
        if (!IsValid) return;

        AP_ErikaState[] _allState = fsm.GetBehaviours<AP_ErikaState>();
        for (int i = 0; i < _allState.Length; i++)
            _allState[i].InitState(this);

        AddListenerDetection();
        AddListenerMovement();
        AddListenerAtk();
        AddListenrPet();
        AddListenerSearch();

        fsm.SetBool(patternParam, true);
        OnFinishInit?.Invoke();
    }

    void AddListenerDetection()
    {
        detection.OnTargetDetected += (target) =>
        {
            atk.SetAtktarget(target);
            transform.LookAt(target.TargetPosition);
            master.SpecificPet.Attack(atk.AtkTarget);
            fsm.SetBool(chaseparam, true);
            fsm.SetBool(patternParam, false);
            fsm.SetBool(searchParam, false);
        };
        detection.OnTargetLost += (target) =>
        {
            Search.SetTarget(target);
            fsm.SetBool(chaseparam, false);
            fsm.SetBool(atkParam, false);
            fsm.SetBool(searchParam, true);
            atk.SetAtktarget(null);
        };
    }

    void AddListenerMovement()
    {
        movements.OnPositionReached += () =>
        {
            fsm.SetBool(waitParam, true);
        };
    }

    void AddListenerAtk()
    {
        atk.OnAttackRange += () =>
        {
            fsm.SetBool(chaseparam, false);
            fsm.SetBool(atkParam, true);
        };
        atk.OnAttackRangelost += () =>
        {
            if (atk.AtkTarget == null) return;
            movements.SetTarget(atk.AtkTarget.TargetPosition.x, transform.position.y, atk.AtkTarget.TargetPosition.z);
            fsm.SetBool(chaseparam, true);
            fsm.SetBool(atkParam, false);
        };
    }

    void AddListenrPet()
    {
        master.SpecificPet.OnPlayerFound += (target) =>
        {
            movements.SetTarget(target.TargetPosition.x, transform.position.y, target.TargetPosition.z);
            fsm.SetBool(chaseparam, true);
        };
    }

    void AddListenerSearch()
    {
        search.OnStopSearch += () =>
        {
            master.SpecificPet.PlayerNotFound();
            fsm.SetBool(searchParam, false);
            fsm.SetBool(patternParam, true);
        };
    }

    private void OnDestroy()
    {
        OnFinishInit = null;
    }
}

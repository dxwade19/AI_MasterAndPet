using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AI_MouseBrain : MonoBehaviour
{
    [SerializeField, Header("Pet FSM")] Animator fsm = null;

    [SerializeField] AI_PetFollow mouvement = null;
    [SerializeField] AP_FightSystem attack = null;
    [SerializeField] AI_Pet pet = null;
    [SerializeField] Ai_MouseAnim mouseAnim = null;
    [SerializeField] AP_Target mouseTarget = null;
    [SerializeField] CS_ConeSight detectionSystem = null;

    [SerializeField, Header("Parameters name In Fsm")] string moveParam = "IsFollowMaster";
    [SerializeField] string attackParam = "IsAttack";
    [SerializeField] string attackMoveParam = "IsMoveToTarget";
    [SerializeField] string deathParam = "IsDeath";
    [SerializeField] string healParam = "IsHeal";
    [SerializeField] string searchParam = "IsSearch";
    IDetection detection = null;

    public Animator FSM => fsm;
    public AI_PetFollow Mouvement => mouvement;
    public AP_FightSystem FightSystem => attack;
    public AI_Pet Pet => pet;
    public Ai_MouseAnim MouseAnim => mouseAnim;
    public CS_ConeSight Detection => detectionSystem;
    public bool IsValid => fsm && mouvement && attack && pet && mouseTarget && mouseAnim && detectionSystem;


    void Start() => Init();

    void Init()
    {
        mouvement = GetComponent<AI_PetFollow>();
        attack = GetComponent<AP_FightSystem>();
        pet = GetComponent<AI_Pet>();
        mouseAnim = GetComponent<Ai_MouseAnim>();
        mouseTarget = GetComponent<AP_Target>();

        if (!IsValid) return;
        InitState();
        InitAnimation();
        //
        InitAttackOrder();
        InitReturnOrder();
        InitSearchOrder();
        FightSystem.OnAttackRangelost += () => FSM.SetBool(attackParam, false);
        InitFightOrder();
    }

    void InitState()
    {
        AI_MouseState[] _allState = fsm.GetBehaviours<AI_MouseState>();
        for (int i = 0; i < _allState.Length; i++)
            _allState[i].InitState(this);
    }
    void InitAnimation()
    {
        mouvement.OnMoving += () => mouseAnim.SetMoveAnim(true);
        mouvement.OnPositionReached += () => mouseAnim.SetMoveAnim(false);
        FightSystem.OnAttackRangelost += () => mouseAnim.SetMoveAnim(true);
        FightSystem.OnAttackRangelost += () => mouseAnim.SetAttackAnim(false);
        FightSystem.OnAtk += () => mouseAnim.SetAttackAnim(true);
    }
    
    void InitAttackOrder()
    {
        pet.OnAtkReceiveOrder += (target) =>
        {
            fsm.SetBool(attackMoveParam, true);
            fsm.SetBool(moveParam, false);
            Mouvement.SetTarget(target);
            FightSystem.SetAtktarget(target);
        };
    }
    void InitReturnOrder()
    {
        pet.OnReturn += () =>
        {
            fsm.SetBool(moveParam, true);
            fsm.SetBool(attackMoveParam, false);
            fsm.SetBool(attackParam, false);
            fsm.SetBool(searchParam, false);
        };
    }
    void InitSearchOrder()
    {
        pet.OnPlayerFound += (target) =>
        {
            fsm.SetBool(searchParam, false);
            pet.Attack(target);
        };
        pet.OnSearchOrder += (targeLastPos) =>
        {
            fsm.SetBool(searchParam, true);
            mouvement.SetTarget(targeLastPos);
        };
        pet.OnPlayerNotFound += () =>
        {
            fsm.SetBool(searchParam, false);
            Pet.Return();
        };
    }
    void InitFightOrder()
    {
        //mouseTarget.OnNeedHeal += (value) => AI_FeedbackManager.Instance?.CreatEffect(transform.position + transform.up, 1);
        mouseTarget.OnDie += () =>
        {
            fsm.SetBool(deathParam, true);
            MouseAnim.SetIsDead(true);
        };
    }

}

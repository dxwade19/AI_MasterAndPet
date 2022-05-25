using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI_Pet : MonoBehaviour, IPet
{
    public event Action<ITarget> OnAtkReceiveOrder = null;
    public event Action OnReturn = null;

    public event Action<Vector3> OnSearchOrder = null;
    public event Action<ITarget> OnPlayerFound = null;
    public event Action OnPlayerNotFound = null;
    

    [SerializeField] IMasterPet masterPet = null;
    [SerializeField] AI_MouseBrain mouseBrain = null;

    public IMasterPet Master => masterPet;
    public AI_MouseBrain MouseBrain => mouseBrain;


    void Start()
    {
        mouseBrain = GetComponent<AI_MouseBrain>();
        Return();
        OnReturn += () => GetComponent<AP_Target>().Life = 100;
    }


    public void Attack(ITarget _target) => OnAtkReceiveOrder?.Invoke(_target);
    public void Return() => OnReturn?.Invoke();
    public void Search(Vector3 _target) => OnSearchOrder?.Invoke(_target);
    


    public void PlayerFound(ITarget _target) => OnPlayerFound?.Invoke(_target);
    public void PlayerNotFound() => OnPlayerNotFound?.Invoke();
}

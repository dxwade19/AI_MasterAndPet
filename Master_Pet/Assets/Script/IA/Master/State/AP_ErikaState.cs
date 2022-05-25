using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AP_ErikaState : StateMachineBehaviour
{
    public event Action OnEnter = null;
    public event Action OnUpdate = null;
    public event Action OnExit = null;

    protected AP_ErikaBrain brain = null;
    //class abstract brain a faire
    public virtual void InitState(AP_ErikaBrain _brain) => brain = _brain;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnUpdate?.Invoke();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();
}

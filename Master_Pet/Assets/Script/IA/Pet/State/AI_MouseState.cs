using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AI_MouseState : StateMachineBehaviour
{
    public event Action OnEnter = null;
    public event Action OnUpdate = null;
    public event Action OnExit = null;

    protected AI_MouseBrain brain = null;
    
    public virtual void InitState(AI_MouseBrain _brain) => brain = _brain;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnUpdate?.Invoke();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();
}

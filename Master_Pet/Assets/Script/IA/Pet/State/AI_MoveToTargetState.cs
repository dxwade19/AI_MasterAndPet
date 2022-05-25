using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MoveToTargetState : AI_MouseState
{
    public override void InitState(AI_MouseBrain _brain)
    {
        base.InitState(_brain);
        OnUpdate += () => brain.Mouvement.SetTarget(brain.FightSystem.AtkTarget);
        OnUpdate += brain.Mouvement.MoveTo;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        brain.Mouvement.OnPositionReached += () => brain.FSM.SetBool("IsAttack", true);
    }

}

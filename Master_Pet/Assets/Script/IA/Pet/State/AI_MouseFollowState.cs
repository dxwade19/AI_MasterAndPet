using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI_MouseFollowState : AI_MouseState
{
    public override void InitState(AI_MouseBrain _brain)
    {
        base.InitState(_brain);
        OnUpdate += () => brain.Mouvement.SetTarget(brain.Mouvement.MasterFollowTargetPos);
        OnUpdate += brain.Mouvement.MoveTo;
    }
}

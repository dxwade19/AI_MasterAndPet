using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ai_AttackTargetState : AI_MouseState
{
    public override void InitState(AI_MouseBrain _brain)
    {
        base.InitState(_brain);
        OnUpdate += brain.FightSystem.UpdateFightSystem;
    }

}

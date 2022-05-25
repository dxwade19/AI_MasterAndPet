using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_PatternState : AP_ErikaState
{
    public override void InitState(AP_ErikaBrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () => brain.Movements.SetTarget(brain.Pattern.PickPoint());
        OnUpdate += () => brain.Movements.MoveTo();
    }
}

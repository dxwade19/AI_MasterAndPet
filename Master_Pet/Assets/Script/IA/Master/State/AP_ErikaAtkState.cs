using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_ErikaAtkState : AP_ErikaState
{
    public override void InitState(AP_ErikaBrain _brain)
    {
        base.InitState(_brain);
        OnUpdate += () => brain.Atk.UpdateFightSystem();
    }
}

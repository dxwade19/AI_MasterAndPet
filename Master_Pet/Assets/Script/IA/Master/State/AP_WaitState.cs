using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_WaitState : AP_ErikaState
{   
    public override void InitState(AP_ErikaBrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () => brain.Fsm.SetFloat(brain.WaitFloatParam, Random.Range(.1f, 1));
        OnExit += () => brain.Fsm.SetBool(brain.WaitParam, false);
    }
}

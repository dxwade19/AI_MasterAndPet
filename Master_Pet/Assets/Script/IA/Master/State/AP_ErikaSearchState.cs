using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_ErikaSearchState : AP_ErikaState
{
    public override void InitState(AP_ErikaBrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
            brain.Search.SetPet(brain.Master.SpecificPet);
            brain.Search.Init();
        };
        OnExit += brain.Search.ResetSearch;
    }
}
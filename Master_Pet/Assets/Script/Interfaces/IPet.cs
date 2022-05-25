using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPet
{
    event Action<ITarget> OnAtkReceiveOrder;
    event Action OnReturn;
    IMasterPet Master { get; }
}

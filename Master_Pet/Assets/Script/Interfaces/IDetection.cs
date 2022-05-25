using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IDetection
{
    event Action<ITarget> OnTargetDetected;
    event Action<Vector3> OnTargetLost;


}

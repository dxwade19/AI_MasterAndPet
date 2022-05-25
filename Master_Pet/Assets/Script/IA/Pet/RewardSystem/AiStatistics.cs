using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct AiStatistics
{
    float winCount;
    float failCount;

    public AiStatistics(float _winCount, float _failCount)
    {
        winCount = _winCount;
        failCount = _failCount;
    }

    public void UpdateStat()
    {

    }

}

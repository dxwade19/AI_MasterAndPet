using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class AI_RewardSystem : MonoBehaviour
{
    [SerializeField] AiStatistics globalStat = new AiStatistics(0, 0);
    [SerializeField] float actualWinStat = 0;
    [SerializeField] float actualLoseStat = 0;

    void Start()
    {

    }

    public void SetStat(AP_ErikaSearchPlayer _searchSystem)
    {
        
    }


}

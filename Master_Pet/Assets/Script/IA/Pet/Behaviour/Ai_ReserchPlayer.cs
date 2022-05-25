using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ai_ReserchPlayer : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10)] float researchRange = 4;

    ITarget target = null;
    Vector3 targetLastPos = Vector3.zero;
    Vector3 targetToMovePos = Vector3.zero;

    public bool IsValid => target != null;


    public void SetTarget(ITarget _target) => target = _target;
    


    void OnDrawGizmos()
    {
        if (!IsValid) return;
        targetLastPos.ToCircle(researchRange, Color.blue);
        targetToMovePos.ToWireSphere(Color.green, 0.1f);
    }
}

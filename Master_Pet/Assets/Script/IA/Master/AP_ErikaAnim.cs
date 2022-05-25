using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_ErikaAnim : MonoBehaviour
{
    [SerializeField] AP_ErikaBrain brain = null;
    [SerializeField] Animator anim = null;
    [SerializeField] AP_ErikaShootSystem shoot = null;

    [SerializeField] string moveParam = "move", atkParam = "atk";
    public bool IsValid => brain && anim && shoot;


    private void Start()
    {
        brain.OnFinishInit += Init;
    }
    void Init()
    {
        brain.Movements.OnMoving += () => SetMoveParam(true);
        brain.Movements.OnPositionReached += () => SetMoveParam(false);
        brain.Atk.OnAttackRange += () => SetMoveParam(false);
        brain.Detection.OnTargetLost += (target) => SetMoveParam(false);
        brain.Atk.OnAttackRangelost += () => SetMoveParam(false);
        brain.Atk.OnAtk += () => anim.SetBool(atkParam, true);
    }

    public void SetMoveParam(bool _param)
    {
        if (anim.GetBool(moveParam) != _param)
            anim.SetBool(moveParam, _param);
    }

    void EndAtk()
    {
        shoot.ShootArrow();
        anim.SetBool(atkParam, false);
    }
}

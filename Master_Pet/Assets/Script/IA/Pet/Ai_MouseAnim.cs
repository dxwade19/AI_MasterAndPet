using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ai_MouseAnim : MonoBehaviour
{
    [SerializeField] Animator anim = null;

    [SerializeField, Header("param Name In Animator Controller")] string walkName = "IsMove";
    [SerializeField] string isAttack = "IsAttack";
    [SerializeField] string isDead = "IsDeath";

    public void SetMoveAnim(bool _status) => anim.SetBool(walkName, _status);
    public void SetAttackAnim(bool _status) => anim.SetBool(isAttack, _status);
    public void SetIsDead(bool _status) => anim.SetBool(isDead, _status);
}

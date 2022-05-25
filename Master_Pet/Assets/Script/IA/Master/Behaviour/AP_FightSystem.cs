using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AP_FightSystem : MonoBehaviour
{
    public event Action OnAtk = null;
    public event Action OnAttackRange = null;
    public event Action OnAttackRangelost = null;

    [SerializeField, Range(.1f, 100)] float atkRange = 1;
    [SerializeField, Range(.1f, 10)] float atkRate = 1;
    [SerializeField, Range(0, 100)] float dmg = 1;

    float atkTimer = 0;

    public float AtkRate => atkRate;

    protected ITarget atkTarget = null;
    public ITarget AtkTarget => atkTarget;

    void Start() => OnAttackRange += UpdateAtkTarget;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        transform.position.ToCircle(atkRange, Color.red);
        if (atkTarget != null)
            Gizmos.DrawLine(transform.position, atkTarget.TargetPosition);
    }

    private void OnDestroy()
    {
        OnAtk = null;
        OnAttackRange = null;
        OnAttackRangelost = null;
    }

    public void SetAtktarget(ITarget _target) => atkTarget = _target;

    public void UpdateFightSystem()
    {
        if (IsAtAtkRange())
            OnAttackRange?.Invoke();
        else OnAttackRangelost?.Invoke();
    }

    public void UpdateAtkTarget()
    {
        if (atkTarget == null || atkTarget.IsDead) return;
        atkTimer -= Time.deltaTime;
        if(atkTimer<0)
        {
            OnAtk?.Invoke();
            atkTarget.SetDamage(dmg);
            Debug.Log($"dammage : {dmg}");
            atkTimer = atkRate;
        }
    }

    bool IsAtAtkRange()
    {
        if (atkTarget == null) return false;
        return Vector3.Distance(transform.position, atkTarget.TargetPosition) < atkRange;
    }
}

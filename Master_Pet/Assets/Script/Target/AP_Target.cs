using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_Target : MonoBehaviour, ITarget
{
    public event Action<bool> OnNeedHeal = null;
    public event Action<float> OnLife = null;
    public event Action OnDie = null;

    [SerializeField] float maxHp = 100;
    [SerializeField, Range(0, 100)] float currentHp = 0;

    public Vector3 TargetPosition => transform.position;

    public bool IsDead => Life == 0;

    public bool NeedHeal => currentHp < 100;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void OnDestroy()
    {
        OnNeedHeal = null;
        OnLife = null;
        OnDie = null;
    }

    public float Life
    {
        get => currentHp;
        set
        {
            currentHp = value;
            currentHp = Mathf.Clamp(Life, 0, 100);
            OnNeedHeal?.Invoke(NeedHeal);
            OnLife?.Invoke(currentHp);
            if (currentHp <= 0)
                OnDie?.Invoke();
        }
    }


    public void AddLife(float _life)
    {
        if (IsDead) return;
        Life += _life;
    }

    public void SetDamage(float _dmg)
    {
        if (IsDead) return;
        Life -= _dmg;
    }
}

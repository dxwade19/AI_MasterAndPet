using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_ArrowBehaviour : MonoBehaviour
{
    ITarget target = null;
    [SerializeField,Range(0,50)] float speed = 15;

    public bool IsValid => target != null;

    private void Update()
    {
        if (!IsValid) return;
        UpdateArrow();
    }

    void UpdateArrow()
    {
        MoveToTarget();
        HitTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.TargetPosition, Time.deltaTime * speed);
        transform.LookAt(target.TargetPosition);
    }

    void HitTarget()
    {
        if (Vector3.Distance(transform.position, target.TargetPosition) < .5f)
            Destroy(this.gameObject);
    }

    public void SetTarget(ITarget _target) => target = _target;
}

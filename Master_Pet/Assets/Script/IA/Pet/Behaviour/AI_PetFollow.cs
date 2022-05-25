using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AI_PetFollow : MonoBehaviour
{
	public event Action OnPositionReached = null;
	public event Action OnMoving = null;

	[SerializeField, Header("Master")] Transform masterPet = null;

	[SerializeField, Header("OffsetFollowMasterPos"), Range(-10, 10)] float posX = 0;
	[SerializeField, Range(-10, 10)] float posY = 0;
	[SerializeField, Range(-10, 10)] float posZ = 0;

	[SerializeField, Header("Info")] Vector3 targetPosition = Vector3.zero;
	[SerializeField, Range(.1f, 10)] float atPosRange = .1f;
	[SerializeField, Range(.5f, 5)] float speed = 2;


	public IMasterPet Master
    {
		get
        {
			if (!masterPet) return null;
			return masterPet.GetComponent<IMasterPet>();
        }
    }
	public Vector3 MasterFollowTargetPos
	{
		get
		{
			if (Master == null) return transform.position;
			return Master.MasterPosition.position + Offset;
		}
	}
	Vector3 Offset => (masterPet.right * posX) + (masterPet.up * posY) + (masterPet.forward * posZ);
	public bool IsAtPosition => Vector3.Distance(transform.position, targetPosition) < atPosRange;


	public void SetTarget(Vector3 _target) => targetPosition = _target;
	public void SetTarget(ITarget _target)
	{
		if (_target == null) return;
		targetPosition = _target.TargetPosition;
	}


	public void MoveTo()
	{
		if (IsAtPosition)
		{
			OnPositionReached?.Invoke();
			return;
		}
		OnMoving?.Invoke();
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
		transform.LookAt(targetPosition);
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(targetPosition, .2f);
		MasterFollowTargetPos.ToWireSphere(Color.magenta);
	}
}

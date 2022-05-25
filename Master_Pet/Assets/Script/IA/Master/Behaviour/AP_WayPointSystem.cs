using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_WayPointSystem : MonoBehaviour
{
    [SerializeField] List<Vector3> waypoints = new List<Vector3>();
    [SerializeField] int indexPoint = -1;

    public Vector3 PickPoint()
    {
        indexPoint++;
        indexPoint %= waypoints.Count;
        return waypoints[indexPoint];
    }

    public void AddPoint()
    {
        Vector3 _point = waypoints.Count == 0 ? Vector3.zero : waypoints[waypoints.Count - 1] + Vector3.forward;
        waypoints.Add(_point);
    }

    public void Clear()
    {
        waypoints.Clear();
    }

    private void OnDrawGizmos()
    {
        if (waypoints.Count == 0) return; 
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (i < waypoints.Count - 1)
                Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            Gizmos.DrawWireCube(waypoints[i], Vector3.one);
        }
        Gizmos.DrawLine(waypoints[0], waypoints[waypoints.Count - 1]);
    }
}

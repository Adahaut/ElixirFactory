using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform transform;

    private int _currentWaypointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        //if (_waiting)
        //{
        //    _waitCounter += Time.deltaTime;
        //    if (_waitCounter >= _waitTime)
        //        _waiting = false;
        //}
        //else
        //{
        //    if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        //    {
        //        transform.position = wp.position;
        //        _waitCounter = 0f;
        //        _waiting = true;

        //        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;

        //    }
        //    else
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, wp.position, VakoomBT.speed * Time.deltaTime);
        //        transform.LookAt(wp.position);
        //    }
        //}

        
        state = NodeState.RUNNING;
        return state;

    }
}

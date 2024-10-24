using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using System;
using System.IO;

public class VakoomPatrol : Node
{
    private Transform _transform;

    private float _WaitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;


    public VakoomPatrol(Transform transform)
    {
        _transform = transform;

    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _WaitTime)
            {
                _waiting = false;
            }
        }
        else
        {
           //make patrol comportement
        }

        state = NodeState.RUNNING;
        return state;
    }


}



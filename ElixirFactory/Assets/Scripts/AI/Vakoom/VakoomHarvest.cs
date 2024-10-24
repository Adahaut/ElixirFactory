using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class VakoomHarvest : Node
{
    private Transform _lastTarget;
    

    public VakoomHarvest(Transform transform)
    {
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if(target !=  _lastTarget)
        {
            //get lake component
        }

        state = NodeState.RUNNING;
        return state;
    }
}

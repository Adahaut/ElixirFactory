using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class VakoomGoToTarget : Node
{
    public Pathfinding pathfinding;
    private Transform _transform;
    private List<Case> path;

    private float _WaitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;


    public VakoomGoToTarget(Transform transform)
    {
        _transform = transform;
        pathfinding = Pathfinding.instance;

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
            MoveAlongPath();
        }

        state = NodeState.RUNNING;
        return state;
    }

    public void MoveAlongPath()
    {
        GetPath(_transform.position, Vector2.zero); //target position
        FollowPath();
    }


    void FollowPath()
    {

        foreach (Case targetCase in path)
        {
            Vector3 targetPosition = new Vector2(targetCase.x, targetCase.transform.position.y);

            while (Vector3.Distance(_transform.position, targetPosition) > 0.1f)
            {
                _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, VakoomBT.speed * Time.deltaTime);
            }
        }
    }

    void GetPath(Vector2 initPos, Vector2 targetPos)
    {
        path = Pathfinding.instance.AStar(Pathfinding.instance.gridModel.grid[(int)initPos.y, (int)initPos.x].GetComponent<Case>(),
            Pathfinding.instance.gridModel.grid[(int)targetPos.y, (int)targetPos.x].GetComponent<Case>());
    }
}

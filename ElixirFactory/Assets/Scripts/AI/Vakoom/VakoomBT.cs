using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VakoomBT : Tree
{
    public static float speed = 2f;

    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform);

        return root;
    }
}

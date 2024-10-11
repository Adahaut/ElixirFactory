using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VakoomPatrolState : IState
{
    private Vector2 targetLakePosition; // Position du lac
    public List<Vector2> lakePosition = new();

    public bool isDrinking = false;
    public bool isOnlake = false;

    public void OnEnter(VakoomStateController sc)
    {
        FindLake(sc);
    }

    public void UpdateState(VakoomStateController sc)
    {
        if (isOnlake)
        {
           sc.ChangeState(sc.vakoomDrinkingState);
        }
        else
        {
            MoveTowardsLake(sc);
        }
    }

    public void OnExit(VakoomStateController sc)
    {
        
    }

    private void FindLake(VakoomStateController sc)
    {

        targetLakePosition = new Vector2(5, 5);
    }

    private void MoveTowardsLake(VakoomStateController sc)
    {
        // Déplacement vers le lac
        float step = sc.speed * Time.deltaTime;
        sc.transform.position = Vector2.MoveTowards(sc.transform.position, targetLakePosition, step);

        // Vérifie si la bête a atteint le lac
        if (Vector2.Distance(sc.transform.position, lakePosition.Count(0)) < 0.1f)
        {
            isOnlake = true;
        }
    }


}
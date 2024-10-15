using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobsBehavior : MonoBehaviour
{
    private List<Case> path = new();
    public float speed = 5f; // Vitesse de déplacement de l'objet
    private Pathfinding pathfinding;
    

    private void Update()
    {
        if (pathfinding == null)
        {
            pathfinding = Pathfinding.instance;
        }
    }

    // Appel pour commencer le déplacement
    public void MoveAlongPath()
    {
        GetPath(Vector2.zero, Vector2.zero);
        StartCoroutine(FollowPath());
    }

    // Coroutine pour suivre le chemin
    IEnumerator FollowPath()
    {
        // Parcours du chemin
        foreach (Case targetCase in path)
        {
            Vector3 targetPosition = new Vector2(targetCase.x, targetCase.transform.position.y);
            
            // Tant que l'objet n'est pas arrivé à la position cible
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Déplace l'objet progressivement vers la position cible
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;  // Attend le frame suivant
            }
        }
    }

    void GetPath(Vector2 initPos, Vector2 targetPos)
    {
        path = Pathfinding.instance.AStar(Pathfinding.instance.gridModel.grid[0, 0].GetComponent<Case>(), 
            Pathfinding.instance.gridModel.grid[10, 10].GetComponent<Case>());
    }
}

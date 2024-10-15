using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GridModel gridModel;
    public static Pathfinding instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Case> AStar(Case startNode, Case targetNode)
    {
        List<Case> openList = new List<Case> { startNode };
        HashSet<Case> closedList = new HashSet<Case>();

        while (openList.Count > 0)
        {
            Case currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost ||
                    openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Case neighbor in GetNeighbors(currentNode))
            {
                if (neighbor.isOccupied || closedList.Contains(neighbor))
                {
                    continue;
                }

                float newGCost = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newGCost < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newGCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }


List<Case> RetracePath(Case startCase, Case endCase)
{
        List<Case> path = new List<Case>();
        Case currentCase = endCase;

        while (currentCase != startCase) {
            path.Add(currentCase);
            currentCase = currentCase.parent;
        }
        path.Add(startCase); // Ajoute la case de départ au chemin
        path.Reverse(); // Inverse le chemin pour qu'il commence par la case de départ


        foreach (Case node in path)
        {
            node.GetComponent<SpriteRenderer>().color = Color.black;
        }
        return path; // Retourne la liste du chemin
    }

    List<Case> GetNeighbors(Case node) {
        List<Case> neighbors = new List<Case>();


        int[,] directions = {
            { 0, 1 },  // Haut
            { 0, -1 }, // Bas
            { -1, 0 }, // Gauche
            { 1, 0 },  // Droite
            { -1, 1 }, // Haut-gauche
            { 1, 1 },  // Haut-droite
            { -1, -1 },// Bas-gauche
            { 1, -1 }  // Bas-droite
        };


        // Vérifie chaque direction
        for (int i = 0; i < directions.GetLength(0); i++) {
            int neighborX = node.x + directions[i, 0];
            int neighborY = node.y + directions[i, 1];

            // Vérifie que la position est bien dans les limites de la grille
            if (neighborX >= 0 && neighborX < gridModel.gridSize && neighborY >= 0 && neighborY < gridModel.gridSize) {
                neighbors.Add(gridModel.grid[neighborX, neighborY].GetComponent<Case>()); // Ajoute le nœud voisin
            }
        }

        return neighbors;
    }

    float GetDistance(Case nodeA, Case nodeB) {
        int distX = Mathf.Abs(nodeA.x - nodeB.x);
        int distY = Mathf.Abs(nodeA.y - nodeB.y);
    
        // Si les deux distances sont égales, c'est un déplacement en diagonale
        return Mathf.Max(distX, distY);
    }

}

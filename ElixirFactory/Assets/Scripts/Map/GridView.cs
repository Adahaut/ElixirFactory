using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    public GameObject casePrefab;
    public GameObject[,] casesGo;

    public void CreateGrid(int gridSize)
    {
        casesGo = new GameObject[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 pos = new Vector3(i, j, 0);
                GameObject caseGo = Instantiate(casePrefab, pos, Quaternion.identity);
                casesGo[i, j] = caseGo;
            }
        }
    }
}
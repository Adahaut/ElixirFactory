using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldGenerator : MonoBehaviour
{
    private GridModel _gridModel;
    private int numberOfFields = 0;
    [SerializeField] private int targetNumberOfFields = 4;

    private void Start()
    {
        _gridModel = GetComponent<GridModel>();
    }


    public void FieldIsPlacable()
    {
        Vector2 randomPos = new Vector2();
        GameObject fieldCase = GenerateField(randomPos);
        do
        {
            int randomX = Random.Range(0, _gridModel.gridSize);
            int randomY = Random.Range(0, _gridModel.gridSize);

            if (_gridModel.grid[randomX, randomY].GetComponent<Case>().name == "Sand")
            {
                randomPos = new Vector2(randomX, randomY);
                GenerateField(randomPos);
                numberOfFields++;
            }
        } while (numberOfFields != targetNumberOfFields);
    }
    private GameObject GenerateField(Vector2 randomPos)
    {
         return Instantiate(_gridModel.casePrefab, randomPos, Quaternion.identity);
    }
}

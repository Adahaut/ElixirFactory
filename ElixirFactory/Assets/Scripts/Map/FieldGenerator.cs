using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldGenerator : MonoBehaviour
{
    private GridModel _gridModel;
    private int NumberOfFields = 0;
    [SerializeField] private int targetNumberOfFields = 4;

    private void Start()
    {
        _gridModel = gameObject.GetComponent<GridModel>();
    }


    public void FieldIsPlacable()
    {
        int randomX;
        int randomY;

        while (NumberOfFields != targetNumberOfFields)
        {
            randomX = Random.Range(0, _gridModel.gridSize);
            randomY = Random.Range(0, _gridModel.gridSize);
            
        }
    }
    public void GenerateField()
    {
    }
}

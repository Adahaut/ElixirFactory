using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    private GridModel _model;
    private GridView _view;


    [Header("   Noise Options")] [Range(0, 1)]
    public float perlinScale = 0.07f;
    public float seed;
    
    
    
    private void Start()
    {

        if (seed == 0)
        {
            seed = Random.Range(0, 10000);
        }
        Debug.Log("Seed: " + seed);
        
         _model = gameObject.GetComponent<GridModel>();
        _model.grid = new GameObject[_model.gridSize, _model.gridSize];
        InitializeGrid();
        
        _view = GetComponent<GridView>();
        _view.CreateGrid(_model);

    }
    
    public void InitializeGrid()
    {
        for (int i = 0; i < _model.gridSize; i++)
        {
            for (int j = 0; j < _model.gridSize; j++)
            {
                _model.grid[i, j] = InstantiatePrefab(new Vector2(i, j));
                _model.grid[i, j].gameObject.name = "Case" + i + "," + j;
            }
        }
    }

    public GameObject InstantiatePrefab(Vector2 position)
    {
        GameObject prefab = Instantiate(_model.casePrefab, position, Quaternion.identity, _model.map); 
        float noiseValue = Mathf.PerlinNoise((position.x + seed)* perlinScale, (position.y + seed) * perlinScale);
        switch (noiseValue)
        { 
            case < 0.7f:
                prefab.GetComponent<Case>().sprite = _model.grassSprite;
                break;
            case >= 0.7f:
                prefab.GetComponent<Case>().sprite = _model.rockyGroundSprite;
                break;
            default:
                break;
        }
        return prefab;
    }

    public bool CheckIfCaseFree(Vector2 coords, Vector2 size)
    {
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if (_model.grid[(int)coords.x + x, (int)coords.y + y].GetComponent<Case>().isOccupied)
                {
                    return false;
                }
            }
        }

        return true;
    }
}

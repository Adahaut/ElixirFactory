using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private GridModel _model;
    private GridView _view;

    
    [Header("   Noise Options")] [Range(0, 1)]
    [SerializeField] private float perlinScale;
    
    
    private void Start()
    {
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
                _model.grid[i, j] = InstiantiatePrefab(new Vector2(i, j));
            }
        }
    }

    public GameObject InstiantiatePrefab(Vector2 position)
    {
        GameObject prefab = Instantiate(_model.casePrefab, position, Quaternion.identity, _model.map);
        
        float noiseValue = Mathf.PerlinNoise(position.x * perlinScale, position.y * perlinScale);
        
        if (noiseValue < 0.5f)
        {
            prefab.GetComponent<Case>().sprite = _model.grassSprite;
        }
        else
        {
            prefab.GetComponent<Case>().sprite = _model.rockSprite;
        }
        return prefab;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private GridModel model;
    private GridView view;

    public int initialGridSize = 20;
    private void Start()
    {
        model = new GridModel(initialGridSize);
        
        
        view = GetComponent<GridView>();
        view.CreateGrid(model.gridSize);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    private GameObject currentBuildPrefab;
    private GameObject currentBuildPreview;
    private BuildProperties currentBuildProperties;
    public Controller controller;
    public GridModel gridModel;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    public void SetBuildPrefab(GameObject buildPrefab)
    {
        currentBuildPrefab = buildPrefab;
        InstantiateBuildPreview(currentBuildPrefab);
    }

    public void InstantiateBuildPreview(GameObject buildPrefab)
    {
        currentBuildPreview = Instantiate(currentBuildPrefab);
        currentBuildProperties = currentBuildPreview.GetComponent<BuildProperties>();
    }

    private void SetBuildPreviewPosition()
    {
        currentBuildPreview.transform.position =
            ClampToNearest((Vector2)camera.ViewportToWorldPoint(controller.mousePos), 1);
        
    }

    private Vector2 ClampToNearest(Vector2 pos, float treshold)
    {
        float t = 1f / treshold;
    
        Vector2 v = ((Vector2)Vector2Int.RoundToInt(pos * t)) / t;
    
        return v;
    }

    private void PlaceBuild()
    {
        currentBuildProperties.SetCoordinates(currentBuildPreview.transform.position);
        currentBuildProperties.SetCoordinatesOfBuildInGrid(gridModel.grid);
        currentBuildPreview = null;
        controller.mouseLeftClick = false;
        InstantiateBuildPreview(currentBuildPrefab);
    }
    
    private void Update()
    {
        if (currentBuildPreview != null)
        {
            SetBuildPreviewPosition();
            if (controller.mouseLeftClick)
            {
                PlaceBuild();
            }
            if (controller.escapePressed)
            {
                Destroy(currentBuildPreview);
                currentBuildPreview = null;
            }
        }
    }
}

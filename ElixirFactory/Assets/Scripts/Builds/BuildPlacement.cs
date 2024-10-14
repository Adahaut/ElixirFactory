using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    private GameObject currentBuildPrefab;
    private GameObject currentBuildPreview;
    public Controller controller;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    public void SetBuildPrefab(GameObject buildPrefab)
    {
        currentBuildPrefab = buildPrefab;
        currentBuildPreview = Instantiate(currentBuildPrefab);
    }

    private void SetBuildPreviewPosition()
    {
        currentBuildPreview.transform.position =
            ClampToNearest((Vector2)camera.ViewportToWorldPoint(controller.mousePos), 1);
        
    }

    private Vector2 ClampToNearest(Vector2 pos, float treshold)
    {
        float t = 1f / treshold;
        Vector2 v = ((Vector2)Vector2Int.FloorToInt(pos * t)) / t;

        float s = treshold * 0.5f;
        v.x += s;
        v.y += s;

        return v;
    }
    
    private void Update()
    {
        if (currentBuildPreview != null)
        {
            SetBuildPreviewPosition();
        }
    }
}

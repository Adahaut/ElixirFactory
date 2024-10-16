using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

public class CameraController : MonoBehaviour
{
    [Header("    Movement Settings")]
    private Vector2 topLeft;
    private Vector2 bottomRight;
    private float widthRatio;
    [SerializeField] private float speed;
    [SerializeField] private float camAccel;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float accelerationPower;
    [SerializeField] private AnimationCurve zoomImpactOnSpeed;

    [Header("    Camera Settings")]
    [SerializeField] Controller _controller;
    private Camera cam;
    
    [Header("    Zoom Settings")] 
    [SerializeField] private float size;
    [SerializeField] private float targetSize;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomPower;
    
    
    public GridModel _gridModel;


    private void Start()
    {
        topLeft = new Vector2(-0.5f, _gridModel.gridSize - 0.5f);
        bottomRight = new Vector2(_gridModel.gridSize - 0.5f, -0.5f);
        widthRatio = (float)Screen.width / (float)Screen.height;
        cam = Camera.main;
    }

    private void Update()
    {
        ZoomCamera();
        MoveCamera();
    }

    public void MoveCamera()
    {
        targetSpeed = _controller.shiftPressed ? camAccel : speed;
        targetSpeed *= zoomImpactOnSpeed.Evaluate(cam.orthographicSize);
        
        if (_controller.shiftPressed)
        {
            smoothSpeed = Mathf.Lerp(smoothSpeed, targetSpeed, accelerationPower * Time.deltaTime);
        }
        else
        {
            smoothSpeed = targetSpeed;
        }
        
      
        
        var camTransform = cam.transform.position + (Vector3)_controller.cameraAxis * (smoothSpeed * Time.deltaTime);
        
        if (camTransform.x - (cam.orthographicSize * widthRatio) < topLeft.x)
        {
            camTransform.x = topLeft.x + (cam.orthographicSize * widthRatio);
        }
        else if (camTransform.x + (cam.orthographicSize * widthRatio) > bottomRight.x)
        {
            camTransform.x = bottomRight.x - (cam.orthographicSize * widthRatio);
        }

        if (camTransform.y + cam.orthographicSize > topLeft.y)
        {
            camTransform.y = topLeft.y - cam.orthographicSize;
        }
        else if (camTransform.y - cam.orthographicSize < bottomRight.y)
        {
            camTransform.y = bottomRight.y + cam.orthographicSize;
        }

        cam.transform.position = camTransform;
    }

    public void ZoomCamera()
    {
        targetSize = Mathf.Clamp(targetSize - _controller.scrollWheel * zoomPower * Time.deltaTime, minSize, maxSize);
        size = Mathf.Lerp(size, targetSize, zoomSpeed * Time.deltaTime);
        cam.orthographicSize = size;
    }
}
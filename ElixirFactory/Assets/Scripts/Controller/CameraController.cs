using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

public class CameraController : MonoBehaviour
{
    [Header("    Movement Settings")]
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;
    private float widthRatio;
    
    [Header("    Camera Settings")]
    [SerializeField] Controller _controller;
    private Camera cam;
    private float speed = 15.0f;
    
    [Header("    Zoom Settings")]
    [SerializeField] private float size;
    [SerializeField] private float targetSize;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomPower;
    

    private void Start()
    {
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
        
        var camTransform = cam.transform.position + (Vector3)_controller.cameraAxis * (speed * Time.deltaTime);;
        if (camTransform.x - (cam.orthographicSize * widthRatio) < topLeft.position.x)
        {
            camTransform.x = topLeft.position.x + (cam.orthographicSize * widthRatio);
        }
        else if (camTransform.x + (cam.orthographicSize * widthRatio) > bottomRight.position.x)
        {
            camTransform.x = bottomRight.position.x - (cam.orthographicSize * widthRatio);
        }

        if (camTransform.y + cam.orthographicSize > topLeft.position.y)
        {
            camTransform.y = topLeft.position.y - cam.orthographicSize;
        }
        else if (camTransform.y - cam.orthographicSize < bottomRight.position.y)
        {
            camTransform.y = bottomRight.position.y + cam.orthographicSize;
        }
        
        cam.transform.position = camTransform; 
    }

    public void ZoomCamera()
    {
        targetSize = Mathf.Clamp(targetSize  - _controller.scrollWheel * zoomPower * Time.deltaTime, minSize, maxSize);
        size = Mathf.Lerp(size, targetSize, zoomSpeed * Time.deltaTime);
        cam.orthographicSize = size;
    }
}

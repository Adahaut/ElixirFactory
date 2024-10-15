using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;

public class CameraController : MonoBehaviour
{
    [Header("    Camera Settings")]
    [SerializeField] Controller _controller;
    private Camera cam;
    private float speed = 4.0f;
    
    [Header("    Zoom Settings")]
    [SerializeField] private float size;
    [SerializeField] private float targetSize;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomPower;
    

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    public void MoveCamera()
    {
        cam.transform.position += (Vector3)_controller.cameraAxis * (speed * Time.deltaTime);
    }

    public void ZoomCamera()
    {
        targetSize = Mathf.Clamp(targetSize  - _controller.scrollWheel * zoomPower * Time.deltaTime, minSize, maxSize);
        size = Mathf.Lerp(size, targetSize, zoomSpeed * Time.deltaTime);
        cam.orthographicSize = size;
    }
}

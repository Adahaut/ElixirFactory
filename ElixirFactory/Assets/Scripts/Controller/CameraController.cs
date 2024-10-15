using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Controller _controller;
    private Camera cam;
    private float speed = 4.0f;

    private void Start()
    {
        
        cam = Camera.main;
    }

    private void Update()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        cam.transform.position += (Vector3)_controller.cameraAxis * (speed * Time.deltaTime);
    }
}

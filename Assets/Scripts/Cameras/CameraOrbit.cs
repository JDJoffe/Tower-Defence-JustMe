﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    //to the camera script is atatched to
    public Camera attachedCamera;

    [Header("Orbit")]
    public Vector3 offset = new Vector3(0, 5f, 0);
    public float xSpeed = 120.0f, ySpeed = 120.0f;
    public float yMinLimit = -20f, yMaxLimit = 80f;
    //distance vars 
    public float distanceMin = .5f, distanceMax = 15f;

    [Header("Collision")]
    public bool cameraCollision = true;
    public bool ignoreTriggers = true;
    public float castRadius = .3f;
    public float castDistance = 1000f;
    public LayerMask hitLayers;
    //old distance to snap back to when no longer obstructed
    private float originalDistance;
    //distance from camera to rawcast
    private float distance;
    private float x, y;

    // Use this for initialization
    void Start()
    {
        // Set ray distance to current distance magnitude of Camera
        originalDistance = Vector3.Distance(transform.position, attachedCamera.transform.position);
        // Get current camera rotation
        Vector3 angles = transform.eulerAngles;
        // Set X and Y degrees to current camera rotation
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        // If Right-Click Is down
        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Rotate the camera based on Mouse X and Mouse Y inputs
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // Clamp the angle using a custom 'ClampAngle' function defined in this script
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // Rotate the transform using euler angles (y for X rotation and x for Y rotation)
            transform.rotation = Quaternion.Euler(y, x, 0);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

     void FixedUpdate()
  
    {
        // Set distance to original distance
        distance = originalDistance;

        // Is camera collision enabled?
        if (cameraCollision)
        {
            // Create a ray starting from target's position and pointing backwards from camera
            Ray camRay = new Ray(transform.position, -transform.forward);
            RaycastHit hit;
            // Shoot a sphere in defined ray direction
            if (Physics.SphereCast(camRay, castRadius, out hit, castDistance, hitLayers, ignoreTriggers ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.Collide))
            {
                // Set current camera distance to hit object's distance
                distance = hit.distance;
            }
        }
        attachedCamera.transform.position = transform.position - transform.forward * distance;
    }
}
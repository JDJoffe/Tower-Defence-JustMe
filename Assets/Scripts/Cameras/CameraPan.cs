using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{

    public Camera atatchedCamera;
    [Header("CameraPanVar")]
    //offset where movement starts %
    public float movementThreshold = 0.25f;
    //speed camera pans
    public float movementSpeed = 20f;

    public float zoomSensitivity = 100f;
    public Vector3 size = new Vector3(10f, 1f, 10f);


    /// <summary>
    /// filters incoming position to keep it within bounds
    /// </summary>
    /// <param name="incomingPos">position that needs filtering</param>
    /// <returns></returns>
    Vector3 GetAdjustedPos(Vector3 incomingPos)
    {
        //store in smaller name
        Vector3 pos = transform.position;
        //getting half size
        Vector3 halfsize = size * 0.5f;
        if (true)
        {

        }
        //X
        if (incomingPos.x > pos.x + halfsize.x) incomingPos.x = pos.x + halfsize.x;
        if (incomingPos.x < pos.x - halfsize.x) incomingPos.x = pos.x - halfsize.x;
        //Y
        if (incomingPos.y > pos.y + halfsize.y) incomingPos.y = pos.y + halfsize.y;
        if (incomingPos.y < pos.y - halfsize.y) incomingPos.y = pos.y - halfsize.y;
        //Z
        if (incomingPos.z > pos.z + halfsize.z) incomingPos.z = pos.z + halfsize.z;
        if (incomingPos.z < pos.z - halfsize.z) incomingPos.z = pos.z - halfsize.z;

        return incomingPos;
    }


    void Movement()
    {
        //create transform for smaller name
        Transform camTransform = atatchedCamera.transform;
        //get mouse to viewport coordinates
        Vector2 mousePoint = atatchedCamera.ScreenToViewportPoint(Input.mousePosition);
        //centre of viewport screen is .5 as it is 0 on top left and 1 on bottom right
        //calculate offset from centre of screen
        Vector2 offset = mousePoint - new Vector2(0.5f, 0.5f);
        //get input only if offset reaches threshhold
        //direction to move the camera
        Vector3 input = Vector3.zero;
        if (offset.magnitude > movementThreshold)
            input = new Vector3(offset.x, 0, offset.y) * movementSpeed;

        //get scroll from axis and multiply by zoomsensitivity
        float inputScroll = Input.GetAxisRaw("Mouse ScrollWheel");
        Vector3 scroll = camTransform.forward * inputScroll * zoomSensitivity;

        //apply movement
        Vector3 movement = input + scroll;
        camTransform.position += movement * Time.deltaTime;

        //filter position with bounds
        camTransform.position = GetAdjustedPos(camTransform.position);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, size);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}

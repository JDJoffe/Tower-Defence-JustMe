using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit_OLD : MonoBehaviour {
    //distance camera is from world zero
    public float distance = 15f;
    //x and y rotation speed
    public float xSpeed = 120f , ySpeed = 120f;
    //x and y rotation limits
    public float yMin = 15f, yMax = 80f;
    //store current x and y rotation
    public float x, y;
    //



	
	
	// Update is called once per frame
	void LateUpdate () {
        //if the left mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            //hide cursor
           
            //get input x and y offsets
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            //offset rotation with mouse X & y
            x += mouseX * xSpeed * Time.deltaTime;
            y -= mouseY * ySpeed * Time.deltaTime;
            //clamp the y between min and max limits
            y = Mathf.Clamp(y, yMin, yMax);

        }
        else
        {
            //show cursor
            Cursor.visible = true;
        }
        //update transform
        transform.rotation = Quaternion.Euler(y, x, 0);
        transform.position = -transform.forward * distance;
	}
}

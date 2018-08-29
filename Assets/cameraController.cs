using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	public float rotSpeed = 4f;
	public float moveSpeed = 4f;
	public Vector3 dir;
	void Update () {
		if (Input.GetKey(KeyCode.W)){
			float step = Time.deltaTime * rotSpeed;
			transform.Rotate(-step, 0, 0);
		}
		if (Input.GetKey(KeyCode.S)){
			float step = Time.deltaTime * rotSpeed;
			transform.Rotate(step, 0, 0);
		}
		if (Input.GetKey(KeyCode.A)){
			float step = Time.deltaTime * rotSpeed;
			transform.Rotate(0, -step, 0);
		}
		if (Input.GetKey(KeyCode.D)){
			float step = Time.deltaTime * rotSpeed;
			transform.Rotate(0, step, 0);
		}
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, 
	        	                            transform.eulerAngles.y, 
	        	                            0);   
		/*
		if (Input.GetMouseButton(0)){
			Cursor.lockState = CursorLockMode.Locked;
			float steps = Time.deltaTime * rotSpeed;
	        transform.Rotate(steps * -Input.GetAxis("Mouse Y"), 
	        				 steps * Input.GetAxis("Mouse X"), 
	        				 0);
	        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 
	        	                                transform.eulerAngles.y, 
	        	                                0);   
    	}*/

    	if (Input.GetMouseButton(1)){
    		float cosY = Mathf.Cos(Mathf.Deg2Rad*transform.eulerAngles.y);
	    	float sinY = Mathf.Sin(Mathf.Deg2Rad*transform.eulerAngles.y);
	    	float cosX = Mathf.Cos(Mathf.Deg2Rad*transform.eulerAngles.x);
	    	float sinX = Mathf.Sin(Mathf.Deg2Rad*transform.eulerAngles.x);

    		float step = Time.deltaTime * moveSpeed;
    		float x = step * Input.GetAxis("Mouse Y") * sinY * -sinX + Input.GetAxis("Mouse X") * -cosY;
    		float y = step * Input.GetAxis("Mouse Y") * -cosX;
    		float z = step * Input.GetAxis("Mouse Y") * cosY * -sinX + Input.GetAxis("Mouse X") * sinY;
	        transform.position += new Vector3 (x,y,z);

	        dir = new Vector3(-cosY, 0, sinY);
    	}

    	Debug.DrawLine(Vector3.zero, dir);
	}
}

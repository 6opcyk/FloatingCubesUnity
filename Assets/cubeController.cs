using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour {
	
	bool isActive = false; 
	Vector3 target;
	Vector3 direction;
	float speed = 10f;
	public mainController mc;

	void Start () {
		gameObject.GetComponent<Animator>().Play("cubeFloat", -1, Random.Range(0.0f, 1.0f));

		mc = GameObject.FindGameObjectWithTag("mainController")
		.GetComponent<mainController>();
		mc.gameMap.Add(transform.position, gameObject);	
	}


	void Update () {
		if (isActive){
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);

	        if (transform.position == target){

	        	if (mc.gameMap.ContainsKey(transform.position + direction)){
	        		isActive = false;
	        		mc.gameMap.Add(transform.position, gameObject);
	        		mc.checkIfOnLine(gameObject, true);
	        	} else {
	        		target += direction;
	        	}
	        }
    	}
	}

	public void setTarget(Vector3 dir){
		if (!isActive){
			if (!mc.gameMap.ContainsKey(transform.position + dir)){
				if (mc.gameMap.ContainsKey(transform.position)){
					mc.gameMap.Remove(transform.position);
				}
				target = transform.position+dir;

				isActive = true;
				direction = dir;
			}
		}
	}
}

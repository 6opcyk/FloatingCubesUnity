using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mainController : MonoBehaviour {

	Vector3 startPos;
	Vector3 endPos;
	Vector3 dir;
	public GameObject checkedCube;
	public Dictionary<Vector3, GameObject> gameMap = new Dictionary<Vector3, GameObject>();

	Vector3 [] dirs;

	void Start () {
		dirs = new Vector3 [] {
			new Vector3 (0, 0, 1), 
			new Vector3 (0, 0, -1),
			new Vector3 (0, 1, 0),
			new Vector3 (0, -1, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (-1, 0, 0),
		};
	}

	void Update () {
		Vector2 mousePos = Input.mousePosition;

		if (Input.GetMouseButtonDown(0)){
			startPos = Camera.main.ScreenToWorldPoint(new Vector3( mousePos.x, 
																   mousePos.y, 
																   Camera.main.nearClipPlane ));
		}
		if (Input.GetMouseButtonUp(0)){
			endPos = Camera.main.ScreenToWorldPoint(new Vector3( mousePos.x, 
															     mousePos.y, 
															     Camera.main.nearClipPlane ));
			dir = (endPos - startPos);

			if (dir.magnitude > 0.01f && checkedCube) {
				moveCube();
			} else {
				checkCube();
			}
		}
	}

	public void moveCube(){
		float[] dirArray = new float[] {Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z)};
		int maxIndex = System.Array.IndexOf(dirArray, Mathf.Max(dirArray));
		float max = dir[maxIndex];
		dir = new Vector3(0,0,0);
		dir[maxIndex] = Mathf.Sign(max);
		checkIfOnLine(checkedCube, false);
		checkedCube.GetComponent<cubeController>().setTarget(dir);
	}

	public void checkCube(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject prevCube = checkedCube;

        if (checkedCube){
        	highlightCube(false, Color.white, checkedCube); 
        	checkIfOnLine(checkedCube, false);
        }
        if (Physics.Raycast(ray, out hit) && 
        	hit.collider.gameObject.tag == "cube" && 
        	prevCube != hit.transform.parent.gameObject){
    			checkedCube = hit.transform.parent.gameObject;
    			highlightCube(true, Color.white, checkedCube); 
    			checkIfOnLine(checkedCube, true);
        } else {
        	checkedCube = null;
        }
	}

	public void checkIfOnLine (GameObject cube, bool state){
		RaycastHit hit;
		foreach (Vector3 dir in dirs){
			if (Physics.Raycast(cube.transform.position, dir, out hit) && 
    			hit.collider.gameObject.tag == "cube"){
				highlightCube(state, Color.blue, hit.transform.parent.gameObject); 
			}
		}
	}

	void highlightCube (bool state, Color color , GameObject cube) {
		Outline outline = cube.GetComponent<Outline>();
		outline.enabled = state;
		outline.OutlineColor = color;			
	}
}

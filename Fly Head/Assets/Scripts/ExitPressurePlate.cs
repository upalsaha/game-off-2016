using UnityEngine;
using System.Collections;

public class ExitPressurePlate : MonoBehaviour {

	public Transform pipeCap;
	Transform rotatePoint;

	Vector3 initialCapPosition;

	bool bodyOnPlate;

	bool capOpen;

	Animator anim;

	// Use this for initialization
	void Start () {
		rotatePoint = pipeCap.GetChild(0);

		bodyOnPlate = false;

		capOpen = false;

		initialCapPosition = pipeCap.position;

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(bodyOnPlate) {
			anim.SetBool("Down", true);
			if(pipeCap.rotation.eulerAngles.z == 0 || pipeCap.rotation.eulerAngles.z >= 270f)
				pipeCap.RotateAround(new Vector3(rotatePoint.position.x, rotatePoint.position.y, rotatePoint.position.z), Vector3.forward, -1);
		} else if(!bodyOnPlate && capOpen) {
			anim.SetBool("Down", false);
			if(pipeCap.rotation.eulerAngles.z <= 360f && pipeCap.rotation.eulerAngles.z >= 355f) {
		    	capOpen = false;
		    	pipeCap.eulerAngles = new Vector3(0f, 0f, 0f);
		    	pipeCap.position = initialCapPosition;
			} else {
				
				pipeCap.RotateAround(new Vector3(rotatePoint.position.x, rotatePoint.position.y, rotatePoint.position.z), Vector3.forward, 4);
			}
		}

	}


	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") {
			bodyOnPlate = true;
			capOpen = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") {
			bodyOnPlate = false;
		}
	}
}

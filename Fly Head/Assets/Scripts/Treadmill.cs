using UnityEngine;
using System.Collections;

public class Treadmill : MonoBehaviour {

	public float bodyTreadmillSpeed;

	public float bodyNotControlledSpeed;

	public float headTreadmillSpeed;

	GameObject treadSwitch;

	// Use this for initialization
	void Start () {
		treadSwitch = GameObject.Find("SwitchTreadmill");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

	}

	void OnCollisionStay2D(Collision2D col) {
		if(!treadSwitch.Equals(null)){

			if((col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") && col.gameObject.GetComponent<BodyController>().bodyBeingControlled) {
				if(treadSwitch.GetComponent<TreadmillDirections>().switched) {
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * -bodyTreadmillSpeed);
				} else {
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * bodyTreadmillSpeed);
				}
			} else if((col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") && !col.gameObject.GetComponent<BodyController>().bodyBeingControlled) {
				if(treadSwitch.GetComponent<TreadmillDirections>().switched) {
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * -bodyNotControlledSpeed);

				} else {
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * bodyNotControlledSpeed);
				}
			} 

			if (col.gameObject.name == "FlyHead" || col.gameObject.name == "FlyHead(Clone)") {
				if(treadSwitch.GetComponent<TreadmillDirections>().switched) {
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * -headTreadmillSpeed);
				} else {
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * headTreadmillSpeed);
				}
			}
		}
	}
}

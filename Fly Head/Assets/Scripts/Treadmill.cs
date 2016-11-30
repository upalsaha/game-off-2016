using UnityEngine;
using System.Collections;

public class Treadmill : MonoBehaviour {

	public float bodyTreadmillSpeed;

	public float bodyNotControlledSpeed;

	public float headTreadmillSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		
	}

	void OnCollisionStay2D(Collision2D col) {
		if((col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") && col.gameObject.GetComponent<BodyController>().bodyBeingControlled) {
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * bodyTreadmillSpeed);
		} else if((col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") && !col.gameObject.GetComponent<BodyController>().bodyBeingControlled) {
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * bodyNotControlledSpeed);
		} 

		if (col.gameObject.name == "FlyHead" || col.gameObject.name == "FlyHead(Clone)") {
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * headTreadmillSpeed);
		}
	}
}

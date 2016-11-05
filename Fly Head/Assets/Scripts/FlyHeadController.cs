using UnityEngine;
using System.Collections;

public class FlyHeadController : MonoBehaviour {

	public float ballSpeed = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 //transform.Translate(5, 0, 0);
 		// transform.Rotate(0, 0, 5);
	}


	void FixedUpdate(){

		  if(Input.GetKey(KeyCode.A)){
		    GetComponent<Rigidbody2D>().AddForce(-Camera.main.transform.right * ballSpeed);
		 }
		  if(Input.GetKey(KeyCode.D)){
		    GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * ballSpeed);
		 }
	}
}

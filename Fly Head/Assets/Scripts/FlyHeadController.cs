using UnityEngine;
using System.Collections;

public class FlyHeadController : MonoBehaviour {

	public float ballSpeed;

	public GameObject Splatter;

	public int splatterTimerCap;
	int splatterTimer;
	bool splatter;

	// Use this for initialization
	void Start () {
		splatterTimer = 0;
		splatter = false;
	}
	
	// Update is called once per frame
	void Update () {
		 //transform.Translate(5, 0, 0);
 		// transform.Rotate(0, 0, 5);

		if(!splatter) {
			if(splatterTimer < splatterTimerCap) {
				splatterTimer++;
			} else {
				splatter = true;
				splatterTimer = 0;
			}
		}
	}


	void FixedUpdate(){
		  if(Input.GetKey(KeyCode.A)){
		    GetComponent<Rigidbody2D>().AddForce(-Camera.main.transform.right * ballSpeed);
		 }
		  if(Input.GetKey(KeyCode.D)){
		    GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.right * ballSpeed);
		 }
	}

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.tag == "Ground") {
			if(splatter) {
				Instantiate(Splatter, new Vector2(transform.position.x, transform.position.y + 2.15f), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
				Instantiate(Splatter, new Vector2(transform.position.x, transform.position.y - 2.15f), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
				Instantiate(Splatter, new Vector2(transform.position.x - 2.0f, transform.position.y), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
				Instantiate(Splatter, new Vector2(transform.position.x + 2.0f, transform.position.y), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
				splatter = false;
			}
		}
	}
}

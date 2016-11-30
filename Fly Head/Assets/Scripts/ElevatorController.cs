using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {

	public GameObject Elevator;
	public float elevator_speed;
	public float peakHeight;

	public bool elevatorGoesDown;

	bool goingUp;

	Animator anim;

	// Use this for initialization
	void Start () {
		goingUp = false;

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!elevatorGoesDown) {
			if(goingUp) {
				Elevator.transform.position += new Vector3(0, elevator_speed, 0);
				if(Elevator.transform.position.y >= peakHeight) {
					anim.SetBool("Down", false);
					goingUp = false;
				}
			}
		} else {
			if(goingUp) {
				Elevator.transform.position -= new Vector3(0, elevator_speed, 0);
				if(Elevator.transform.position.y <= peakHeight) {
					anim.SetBool("Down", false);
					goingUp = false;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && (col.gameObject.name == "FlyHead" || col.gameObject.name == "FlyHead(Clone)")) {
			goingUp = true;
			anim.SetBool("Down", true);
		}
	}

}

using UnityEngine;
using System.Collections;

public class HorizontalElevator : MonoBehaviour {

	public GameObject Elevator;
	public float elevator_speed;
	public float extensionCap;
	public bool extendsLeft;
	public int waitTime;

	float initialX;
	int waitCounter;
	public bool Extending;
	public bool Retracting;
	public bool Waiting;

	// Use this for initialization
	void Start () {
		initialX = Elevator.transform.position.x;

		Extending = false;
		Retracting = false;
		Waiting = false;
		waitCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(Extending && extendsLeft) {
			Elevator.transform.position += new Vector3(-elevator_speed, 0, 0);
			if(Elevator.transform.position.x <= extensionCap) {
				Extending = false;
				Waiting = true;
			}
		} else if (Extending && !extendsLeft) {
			Elevator.transform.position += new Vector3(elevator_speed, 0, 0);
			if(Elevator.transform.position.x >= extensionCap) {
				Extending = false;
				Waiting = true;
			}
		}


		if(Retracting && extendsLeft) {
			Elevator.transform.position += new Vector3(elevator_speed, 0, 0);
			if(Elevator.transform.position.x >= initialX) {
				Retracting = false;
			}
		} else if(Retracting && !extendsLeft) {
			Elevator.transform.position += new Vector3(-elevator_speed, 0, 0);
			if(Elevator.transform.position.x <= initialX) {
				Retracting = false;
			}
		}

		if(Waiting){
			if(waitCounter < waitTime) {
				waitCounter++;
			} else {
				Waiting = false;
				Retracting = true;
				waitCounter = 0;
			}
		}

	}


	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			Extending = true;
		}
	}
}

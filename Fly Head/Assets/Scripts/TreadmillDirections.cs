using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreadmillDirections : MonoBehaviour {

	public List<GameObject> Treadmills;


	bool buttonPressed;

	bool buttonDown;

	public int buttonCap;
	int buttonTimer;

	Animator anim;

	public bool switched;

	// Use this for initialization
	void Start () {
		buttonDown = false;
		buttonPressed = false;

		buttonTimer = 0;

		anim = GetComponent<Animator>();

		switched = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(buttonPressed) {

			if(switched) {
				switched = false;
			} else {
				switched = true;
			}

			foreach (GameObject treadmill in Treadmills) {
				foreach(Transform child in treadmill.transform) {
					if(switched) {
						child.GetComponent<Animator>().SetBool("Reverse", true);
					} else {
						child.GetComponent<Animator>().SetBool("Reverse", false);
					}
				}
				
				if(switched) {
						treadmill.GetComponent<Animator>().SetBool("Reverse", true);
					} else {
						treadmill.GetComponent<Animator>().SetBool("Reverse", false);
				}
			}

			buttonDown = true;
			buttonPressed = false;
		}

		if(buttonDown) {
			buttonTimer++;
			if(buttonTimer > buttonCap) {
				buttonDown = false;
				buttonTimer = 0;
				anim.SetBool("Down", false);
			}
		}
	}


	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && !buttonDown && !buttonPressed && (col.gameObject.name == "FlyHead" || col.gameObject.name == "FlyHead(Clone)")) {
			buttonPressed = true;
			anim.SetBool("Down", true);
		}
	}
}

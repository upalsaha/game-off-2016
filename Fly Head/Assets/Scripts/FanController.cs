﻿using UnityEngine;
using System.Collections;

public class FanController : MonoBehaviour {

	public float fanSpeed;

	bool playerAffected;

	public Transform Player;


	// Use this for initialization
	void Start () {
		Player = (GameObject.FindWithTag("Player")).transform;

		playerAffected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		Player = (GameObject.FindWithTag("Player")).transform;

		if(playerAffected) {
			float distanceToBottom = Mathf.Abs(Player.position.y - transform.GetChild(0).transform.position.y);
			Player.GetComponent<Rigidbody2D>().AddForce(Camera.main.transform.up * (1.9f/distanceToBottom) * fanSpeed);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player" && col.gameObject.name != "Body" && col.gameObject.name != "Body(Clone)") {
			playerAffected = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {

		if(col.gameObject.tag == "Player" && col.gameObject.name != "Body" && col.gameObject.name != "Body(Clone)") {
			playerAffected = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") {
			if(!col.gameObject.GetComponent<BodyController>().bloodDrops)
				col.gameObject.GetComponent<BodyController>().deadAF = true;
		}
	}
}

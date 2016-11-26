using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour {

	public int killTimerCap;
	bool playerDead;
	int killTimer;

	public Object thisLevel;

	// Use this for initialization
	void Start () {
		killTimer = 0;
		playerDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerDead) {
			if(killTimer < killTimerCap) {
				killTimer++;
			} else {
				SceneManager.LoadScene(thisLevel.name);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) { 
		if(col.gameObject.tag == "Player") {

   			col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
   			playerDead = true;

   			if(col.gameObject.name == "Body" || col.gameObject.name == "Body(Clone)") {
   				col.gameObject.GetComponent<BodyController>().bodyBeingControlled = false;
   				col.gameObject.GetComponent<BodyController>().deadAF = true;
   			}
   		}
	}
}

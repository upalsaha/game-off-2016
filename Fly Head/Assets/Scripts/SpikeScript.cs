using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour {

	public int killTimerCap;
	
	bool playerDead;
	int killTimer;

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
				SceneManager.LoadScene("Fan_Spike_Level1");
			}
		}
	}


	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
   			col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
   			playerDead = true;
   		}
	}

}

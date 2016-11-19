using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour {

	public int killTimerCap;
	public GameObject Splatter;
	
	bool playerDead;
	int killTimer;

	System.Random rand;

	// Use this for initialization
	void Start () {

		rand = new System.Random();

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

			for(int i = 0; i < 15; i++) {
				float splatterX = rand.Next(-4, 5);
				float splatterY = rand.Next(-4, 5);

				Instantiate(Splatter, new Vector2(transform.position.x + splatterX, transform.position.y + splatterY), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
			}

   			col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
   			playerDead = true;
   		}
	}

}

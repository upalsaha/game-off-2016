using UnityEngine;
using System.Collections;

public class BloodDrop : MonoBehaviour {

	public GameObject Splatter;

	int colliderCounter;

	// Use this for initialization
	void Start () {
		colliderCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(colliderCounter > 5) {
			GetComponent<BoxCollider2D>().enabled = true;
		} else {
			colliderCounter += 1;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Fan") {
			Instantiate(Splatter, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
			Destroy(gameObject);
		}	
	}
}

using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

	int timer;
	public int timer_cap;

	// Use this for initialization
	void Start () {
		
		timer = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if(timer > timer_cap)
			Destroy (transform.gameObject);


		timer++;
	
	}
}

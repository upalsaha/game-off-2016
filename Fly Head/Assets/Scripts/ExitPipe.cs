using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitPipe : MonoBehaviour {

	public Object nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			SceneManager.LoadScene(nextLevel.name);
		}
	}
}

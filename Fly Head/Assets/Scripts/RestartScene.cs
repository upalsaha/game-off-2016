using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class RestartScene : MonoBehaviour {

	public Object thisLevel;

	public int currentLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(currentLevel);
		}
	}
}

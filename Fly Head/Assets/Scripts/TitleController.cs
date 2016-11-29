using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {
    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 5.0f;
    private float startTime;
    public SpriteRenderer sprite;

    public GameObject title;

    public GameObject splatter;

    public int sceneTimer;

    public Object firstLevel;


    void Start() {
        startTime = Time.time;

        sprite = title.GetComponent<SpriteRenderer>();

        sceneTimer = 0;
    }

    void Update() {
    	sceneTimer++;

 		if(sceneTimer == 300) {
 			Instantiate(splatter, new Vector2(80.7612f, 166.9041f), Quaternion.identity);
 		} else if(sceneTimer == 310) {
 			Instantiate(splatter, new Vector2(83.66428f, 166.3702f), Quaternion.identity);
 		} else if(sceneTimer == 320) {
 			Instantiate(splatter, new Vector2(86.77f, 166.77f), Quaternion.identity);
 		} else if (sceneTimer == 330) {
 			Instantiate(splatter, new Vector2(89.71f, 166.3f), Quaternion.identity);
 		} else if (sceneTimer == 420) {
 			SceneManager.LoadScene(firstLevel.name);
 		}

        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(minimum, maximum, t));        
    }
}
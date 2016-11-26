using UnityEngine;
using System.Collections;

public class SplatterScript : MonoBehaviour {

	public LayerMask whatIsSurface;
	bool parentFound;

	BoxCollider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider2D>();
		parentFound = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(!parentFound) {
			Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x + collider.size.x, transform.position.y + collider.size.y), whatIsSurface);

			for(int i = 0; i < colliders.Length; i++) {
				Collider2D col = colliders[i];
				if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Fan") {
					transform.parent = col.gameObject.transform;
					parentFound = true;
					break;
				}
			}
		}


		if(!parentFound)
			Destroy(gameObject);

	}


}

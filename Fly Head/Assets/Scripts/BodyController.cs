using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour {

	public bool bodyBeingControlled;

	public GameObject FlyHead;

	public float jumpForce;
	public bool facingRight = true;
	public float maxSpeed = 10f;
	public float horizontalVelocity;

	public bool grounded = false;

	public Transform groundCheck; 
	float groundRadius = 0.25f;    
	public LayerMask whatIsGround;

	int headTimer;
	public int headTimerCap;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		headTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) && bodyBeingControlled){
			anim.SetBool ("RemovingHead", true); 

		    bodyBeingControlled = false;

		 }


		if (bodyBeingControlled) {
			if (grounded && Input.GetKeyDown (KeyCode.Space)) { // if grounded and pushing space
		     	anim.SetBool ("Ground", false); //we're no longer grounded
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce)); 
			}
		}

		if(anim.GetBool("RemovingHead")) {

			headTimer++;

			if(headTimer > headTimerCap) {

			    GetComponent<BoxCollider2D>().enabled = false;
			    GetComponent<EdgeCollider2D>().enabled = true;

			    Instantiate(FlyHead, new Vector2(transform.position.x + 0.25f, transform.position.y + 2.25f), Quaternion.identity);

			    gameObject.tag = "Untagged";
			    FlyHead.tag = "Player";

			    anim.SetBool("RemovingHead", false);
			    anim.SetBool("BodyFalling", true);
			    headTimer = 0;

			}
			
		}
	}

	void FixedUpdate() {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		anim.SetBool ("Ground", grounded); 

		anim.SetFloat ("VSpeed", GetComponent<Rigidbody2D> ().velocity.y); 


		float move = Input.GetAxis ("Horizontal");
		horizontalVelocity = move;

		if(bodyBeingControlled) {

			anim.SetFloat ("Speed", Mathf.Abs (move));

			GetComponent<Rigidbody2D>().velocity = new Vector2 ( (move * maxSpeed), GetComponent<Rigidbody2D>().velocity.y);
		
			if (move > 0 && !facingRight)
				Flip ();
			else if (move < 0 && facingRight)
				Flip ();

		}
	}


	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player")
			Debug.Log("Hello");
	}


	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}

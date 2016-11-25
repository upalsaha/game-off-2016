using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BodyController : MonoBehaviour {

	public bool bodyBeingControlled;

	public GameObject FlyHead;

	public float jumpForce;
	public bool facingRight = true;
	public float maxSpeed = 10f;
	public float horizontalVelocity;

	public bool grounded = false;

	public Transform groundCheck; 
	float groundRadius = 0.5f;    
	public LayerMask whatIsGround;

	int headTimer;
	public int headTimerCap;

	bool bodyReset;
	int bodyResetCounter;
	public int bodyResetCap;

	bool inputBlocked;
	int inputBlockCounter;
	public int inputBlockCap;

	public bool hasBeenActivated;


	public GameObject Splatter;
    public int splatterCap;
    public int splatterInterval;
	int splatterCount;
	bool isSplattering;

	public bool deadAF;

	public GameObject DummyPlayer;

	public Transform BloodDrop;
	public float bloodForce;
	public bool bloodDrops;
    int bloodDropCounter;

    int killTimer;
    public int killTimerCap;


    public Object thisLevel;

	System.Random rand;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		headTimer = 0;

		bodyReset = false;
		bodyResetCounter = 0;

		inputBlocked = false;
		inputBlockCounter++;

		anim.SetBool("HasBeenActivated", hasBeenActivated);

		rand = new System.Random();

		isSplattering = false;

		deadAF = false;

		bloodDrops = false;

		bloodDropCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(!bloodDrops)
			anim.SetBool("DeadAF", deadAF);


		if(isSplattering || deadAF) {

			if(deadAF)
				splatterInterval += 8;

			for(int i = 0; i < splatterInterval; i++) {
				splatterCount++;
				if(splatterCount < splatterCap) {
					float splatterX = rand.Next(-8, 15);
					float splatterY = rand.Next(-8, 15);
					if(deadAF) {
						splatterY = -2;
						splatterX = rand.Next(-5, 6);

					}

					Instantiate(Splatter, new Vector2(transform.position.x + splatterX, transform.position.y + splatterY), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)) );
				} else {
					splatterCount = 0;
					isSplattering = false;	
				}
			}

			if(deadAF) {
				deadAF = false;
				bloodDrops = true;
				bodyBeingControlled = false;
				gameObject.tag = "Untagged";
				GameObject newDummy = (GameObject)Instantiate(DummyPlayer, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
				DummyPlayer = newDummy;
			}
		}

		bloodDropCounter += 1;
		if(bloodDrops && bloodDropCounter % 10 == 0) {
			int xForce = rand.Next(-100, 100);
			int xPos = rand.Next(-2, 3);
			Transform TemporaryTransform = (Transform)Instantiate (BloodDrop, new Vector2 (DummyPlayer.transform.position.x + xPos, DummyPlayer.transform.position.y), Quaternion.identity);
			TemporaryTransform.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((float)xForce, bloodForce));
			TemporaryTransform.GetComponent<Rigidbody2D> ().AddTorque ((TemporaryTransform.GetComponent<Rigidbody2D> ().angularVelocity + 1) * 400, ForceMode2D.Force);
			killTimer += 10;

			if(killTimer >= killTimerCap) {
				SceneManager.LoadScene(thisLevel.name);
			}
		}

		if(!inputBlocked && Input.GetKeyDown(KeyCode.E) && bodyBeingControlled){
			inputBlocked = true;
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

			    Instantiate(FlyHead, new Vector2(transform.position.x + 0.25f, transform.position.y + 7.75f), Quaternion.identity);

			    isSplattering = true;

			    gameObject.tag = "Untagged";
			    FlyHead.tag = "Player";

			    anim.SetBool("RemovingHead", false);
			    anim.SetBool("BodyFalling", true);
			    headTimer = 0;

			}
			
		}


		if(bodyReset) {
			bodyResetCounter++;
			if(bodyResetCounter >= bodyResetCap) {
				bodyResetCounter = 0;
				bodyBeingControlled = true;
				bodyReset = false;
			}
		}

		if(inputBlocked) {
			inputBlockCounter++;
			if(inputBlockCounter >= inputBlockCap) {
				inputBlocked = false;
				inputBlockCounter = 0;
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


	void OnTriggerStay2D(Collider2D col) {
		if(!inputBlocked && col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) {
			inputBlocked = true;
			anim.SetBool("BodyFalling", false);
			Destroy(col.gameObject);
			gameObject.tag = "Player";
			bodyReset = true;

			GetComponent<BoxCollider2D>().enabled = true;
			GetComponent<EdgeCollider2D>().enabled = false;

			if(!hasBeenActivated) {
				hasBeenActivated = true;
				anim.SetBool("HasBeenActivated", hasBeenActivated);
			}
		}
	}


	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}

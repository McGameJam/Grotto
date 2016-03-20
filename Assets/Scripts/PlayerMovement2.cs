using UnityEngine;
using System.Collections;

public class PlayerMovement2 : MonoBehaviour {

    private bool facingRight = true;
    private bool jump = false;
    public float moveVelocity = 3.5f;
    public float maxSpeed = 3.5f;
    public float jumpForce = 240f;
	public static int counter = 0;
    public bool player1;

    public Canvas deadUI;

	private AudioSource audio;

    private Vector3 freezePos;
    private bool frozen = false;

    private PlayerMovement2 frozenOther;
    private bool touchingFrozenOther = false;

    private bool grounded = false;
    private Rigidbody rb;
    
    private BoxCollider c;
	private Animator a;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<BoxCollider>();
		a = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
        deadUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        string yAxis = (player1) ? "Vertical" : "Vertical2";
        if (Input.GetAxis(yAxis) > 0 && grounded)
        {
            
            jump = true;
        }

        if (!player1 && Input.GetKeyDown(KeyCode.Space))
        {
			if (frozen) {
				unfreeze ();
				PlayerMovement2[] pls = FindObjectsOfType (typeof(PlayerMovement2)) as PlayerMovement2[];

				foreach (PlayerMovement2 pl in pls) {
					if (pl != this) {
						transform.position = new Vector3 (pl.transform.position.x, pl.transform.position.y + 1.3f, pl.transform.position.z);
					}
				}
			}
			else {
				freeze ();
				audio.Play ();
			}
        }
    }

    void FixedUpdate()
    {
        if (frozen)
        {
            rb.velocity = Vector3.zero;
            transform.position = freezePos;
			a.SetBool ("Frozen", true);
            //rb.isKinematic = true;
        }
        else {
			if(!player1)
				a.SetBool ("Frozen", false);
            string xAxis = (player1) ? "Horizontal" : "Horizontal2";
            float h = Input.GetAxis(xAxis);

			if (h != 0)
				a.SetBool ("Walk", true);
			else {
				a.SetBool ("Walk", false);
				if (jump) {
					a.SetBool ("Jump", true);
				} else {
					a.SetBool ("Jump", false);
				}
			}

            //if (h * rb.velocity.x < maxSpeed)
                rb.velocity = new Vector2(h* moveVelocity, rb.velocity.y);

            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();

            if (jump)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                jump = false;
                grounded = false;
            }

            //Prevent weird slipping under other character
            if (touchingFrozenOther)
            {
                if (Mathf.Abs(frozenOther.transform.position.y - transform.position.y) < c.size.y - 0.6)
                {
                    if (transform.position.z < frozenOther.transform.position.z && h >= 0)
                        transform.position = new Vector3(transform.position.x, frozenOther.transform.position.z - c.size.z / 2);
                    else if (transform.position.z > frozenOther.transform.position.z && h <= 0)
                        transform.position = new Vector3(transform.position.x, transform.position.y, frozenOther.transform.position.z + c.size.z / 2);
                }
            }
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        else if (other.CompareTag("Player"))
        {
            PlayerMovement2 otherPlayer = collision.gameObject.GetComponent<PlayerMovement2>();

            if (otherPlayer.frozen)
            {
                frozenOther = otherPlayer;
                touchingFrozenOther = true;
            }

            if (transform.position.y - otherPlayer.transform.position.y >= 0.55f) //Jump on other player's head
                grounded = true;
        }
        else if (other.CompareTag("Enemy") && !frozen)
        {
			Die ();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag("Player"))
            touchingFrozenOther = false;
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag ("Portal")) {
			other.gameObject.GetComponent<AudioSource>().Play();
			transform.position = new Vector3 (-2.1f, 6.29f, 0);
		} else if (other.CompareTag ("Kill")) {
			Die ();
		} else if (other.CompareTag ("Point") && player1) {
			audio.Play ();
			EnemyMovement[] enems = FindObjectsOfType(typeof(EnemyMovement)) as EnemyMovement[];

			foreach (EnemyMovement e in enems) {
				Destroy(e.gameObject);
			}

			Destroy(other.gameObject);
		}
    }

    public void freeze()
    {
        frozen = true;
        freezePos = transform.position;
        P1Attack.canFire = false;
    }

    public void unfreeze()
    {
        frozen = false;
        P1Attack.canFire = true;
    }

	public static void Die() {
		PlayerMovement2[] pls = FindObjectsOfType (typeof(PlayerMovement2)) as PlayerMovement2[];

		foreach (PlayerMovement2 pl in pls) {
			pl.a.SetBool ("Dead", true);
		}
		pls[0].deadUI.gameObject.SetActive (true);
		print ("Score: " + PlayerMovement2.counter);
		//Time.timeScale = 0;
	}
}

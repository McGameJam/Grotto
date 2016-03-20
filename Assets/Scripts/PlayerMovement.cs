using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    private BoxCollider c;
    public Canvas deadUI;

    private float distToGround;
    private float lastJumpTime;

    private Vector3 freezePos;
    private bool frozen = false;

    private PlayerMovement frozenOther;
    private bool touchingFrozenOther = false;

    public bool player1;
    public float speed;
    public float jumpTime;
    public float jumpSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<BoxCollider>();
        distToGround = c.bounds.extents.y;
        deadUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!player1 && Input.GetKeyDown(KeyCode.Space))
        {
            if (frozen) unfreeze();
            else freeze();
        }
    }
	
	// Update is called once per physics update
	void FixedUpdate () {
        if (frozen)
        {
            rb.velocity = Vector3.zero;
            transform.position = freezePos;
            //rb.isKinematic = true;
        }
        else {

            //rb.isKinematic = false;

            string xAxis = (player1) ? "Horizontal" : "Horizontal2";
            string yAxis = (player1) ? "Vertical" : "Vertical2";

            float x = Input.GetAxis(xAxis);
            transform.Translate(new Vector3(0, 0, x * speed * Time.deltaTime));
            //rb.AddForce(new Vector3(x * speed * 10, 0, 0));

            //Jumping
            if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f) && Time.time - lastJumpTime > jumpTime)
            {
                if (Input.GetAxis(yAxis) > 0)
                {
                    rb.AddForce(new Vector3(0, jumpSpeed, 0));

                    lastJumpTime = Time.time;
                }
            }

            //Prevent weird slipping under other character
            if (touchingFrozenOther)
            {
                if (Mathf.Abs(frozenOther.transform.position.y - transform.position.y) < c.size.y - 0.6)
                {
                    if (transform.position.z < frozenOther.transform.position.z && x >= 0)
                        transform.position = new Vector3(transform.position.x, frozenOther.transform.position.z - c.size.z / 2);
                    else if(transform.position.z > frozenOther.transform.position.z && x <= 0)
                        transform.position = new Vector3(transform.position.x, transform.position.y, frozenOther.transform.position.z + c.size.z / 2);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag("Player") && collision.gameObject.GetComponent<PlayerMovement>().frozen)
        {
            frozenOther = collision.gameObject.GetComponent<PlayerMovement>();
            touchingFrozenOther = true;
        }
        else if(other.CompareTag("Enemy"))
        {
            deadUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Collider other = collision.collider;
        if(other.CompareTag("Player"))
            touchingFrozenOther = false;
    }

    public void freeze()
    {
        frozen = true;
        freezePos = transform.position;
    }

    public void unfreeze()
    {
        frozen = false;
    }
}

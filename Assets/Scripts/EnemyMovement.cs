using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    private BoxCollider c;
    private Rigidbody rb;

    private float lastJumpTime;

    public bool enableMovement = false;
	public float moveSpeed = 5.0f;
    public float jumpMoveSpeed;
    public float jumpSpeed;
    public float distFromWall;
	public float avgPosition;

	private float lifeSpan = 20;
	private float initTime;

    public bool fromRight = true;

    void Start()
    {
        c = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
		initTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
		if (Time.time - initTime > lifeSpan) {
			Destroy (gameObject);
		}
		if (enableMovement) {
            if (Physics.Raycast(transform.position, -Vector3.up, 0.6f)) //Grounded
            {
                bool theresAWall = Physics.Raycast(transform.position, (!fromRight)? Vector3.right: -Vector3.right, distFromWall);
                if (theresAWall && Time.time - lastJumpTime > 0.1f) //Theres a wall in the movement direction and we are grounded: Jump
                {
                    rb.AddForce(new Vector3(0, jumpSpeed, 0));
                    lastJumpTime = Time.time;
                }
			//	avgPosition = (GameObject.Find("Player01").transform.position.x + GameObject.Find("Player02").transform.position.x)/2;
			//	print (avgPosition);
			//	if (transform.position.x > GameObject.Find("Player01").transform.position.x) {
			//		transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
			//	} 
			//	else if (transform.position.x <= GameObject.Find("Player01").transform.position.x) {
			//		transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
			//	}
                transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);

            }

            transform.Translate(-Vector3.right * jumpMoveSpeed * Time.deltaTime);
        }

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Floor")) {
			enableMovement = true;
		}
	}
    void OnTriggerEnter(Collider col)
    {
        print (col.gameObject.tag);
        if (col.gameObject.CompareTag("Hole"))
        {
            transform.Translate(-Vector3.right*jumpMoveSpeed * Time.deltaTime);
        }
        if (col.gameObject.CompareTag("Attack"))
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}


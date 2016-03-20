using UnityEngine;
using System.Collections;

public class P1AttackMovement : MonoBehaviour {

	float speed = 5.0f;

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.localScale.z == 1) {
			transform.Translate (speed * Time.deltaTime, 0, 0);
			if (Physics.Raycast (transform.position, Vector3.right, 0.3f))
				Destroy (gameObject);
		} 
		else if (player.transform.localScale.z == -1) {
			transform.Translate (speed * Time.deltaTime, 0, 0);
			if (Physics.Raycast (transform.position, Vector3.left, 0.3f))
				Destroy (gameObject);
		}
	}


		
}

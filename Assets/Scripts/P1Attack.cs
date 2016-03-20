using UnityEngine;
using System.Collections;

public class P1Attack : MonoBehaviour {

	public GameObject player;
	public GameObject attack;

	private AudioSource audio;

	private Animator a;

	public float delay = 1.0f;
	public float speed = 5.0f;
	public float attackRange;
	private float lastTime;
	private PlayerMovement2 player2;
	private GameObject attackSpawn;
	public static bool canFire = true;
	// Use this for initialization
	void Start () {

		a = transform.parent.GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (canFire) {
			if (Time.time - lastTime > delay) {
				if (Input.GetKeyUp(KeyCode.RightShift))
				{
					a.SetTrigger ("Attack");
					audio.Play ();

					if (player.transform.localScale.z == 1) {
						transform.eulerAngles = new Vector3 (0, 0, 0);

					} else if(player.transform.localScale.z == -1){
						transform.eulerAngles = new Vector3 (0, 180, 0);
					}
					attackSpawn = (GameObject) Instantiate(attack, transform.position, transform.rotation);

					lastTime = Time.time;

					Destroy (attackSpawn, attackRange);
				}
			}
		}





	}
}

using UnityEngine;
using System.Collections;

public class ORBERKILL : MonoBehaviour {

	public static bool spawn = false;

	private float delay = 50;

	public float lastTime;

	// Use this for initialization
	void Start () {
		lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime > delay) {
			PlayerMovement2.Die ();
		}
	}
}

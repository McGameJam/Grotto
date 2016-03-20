using UnityEngine;
using System.Collections;

public class PointGenerator : MonoBehaviour {

    public GameObject point;

	public static GameObject currentPoint;

    private Vector3[] positions = new Vector3[]
    {
        new Vector3(-13f, 2.3f, 0),
        new Vector3(-12.0f, 4.3f, 0),
		new Vector3(-5.5f, 4.20f, 0),  //heh
        new Vector3(-1.0f, 6.7f, 0),
        new Vector3(0.4f, -1.8f, 0),
        new Vector3(5.5f, -2.6f, 0),
        new Vector3(8.9f, 4.4f, 0),
        new Vector3(9.9f, 8.3f, 0),
        new Vector3(19.5f, 3.2f, 0),
        new Vector3(26.0f, 6.0f, 0),
        new Vector3(11.0f, 4.4f, 0)
    };

    //private float lastTime;
    //private float delay = 10;


	void Start() {
		SpawnOrb ();
	}
	// Update is called once per frame
	void Update () {
		if(!currentPoint) {
			SpawnOrb ();
		}
    }

	void SpawnOrb()
	{
		int i = (int)Random.Range (0, positions.Length);
		currentPoint = (GameObject)Instantiate (point, positions [i], transform.rotation);
	}
}

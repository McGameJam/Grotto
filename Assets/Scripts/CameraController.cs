using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public PlayerMovement2 p1;
    public PlayerMovement2 p2;
    public float minCamZ = 7f;
	
	// Update is called once per frame
	void Update () {
        float zValue = - Mathf.Min(14, Mathf.Max(minCamZ, (Mathf.Pow(p1.transform.position.x - p2.transform.position.x, 2) + Mathf.Pow(p1.transform.position.y - p2.transform.position.y, 2)) / (32f*32f) * 100f));
        transform.position = new Vector3((p1.transform.position.x + p2.transform.position.x) / 2, (p1.transform.position.y + p2.transform.position.y) / 2, zValue);
	}
}

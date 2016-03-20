using UnityEngine;
using System.Collections;

public class RockOscillation : MonoBehaviour {

    public float amplitude = 0.5f;

    private Vector3 initPos;
    private float offset;

    void Start()
    {
        initPos = transform.position;
        offset = Random.Range(0, Mathf.PI * 2);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, initPos.y + amplitude*Mathf.Sin(Time.time + offset), transform.position.z);
	}
}

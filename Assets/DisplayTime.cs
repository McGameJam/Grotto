using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour {

	Text t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text> (); 
	}
	
	// Update is called once per frame
	void Update () {
		t.text = "Temps restant: " + Mathf.Round(50 - (Time.time - PointGenerator.currentPoint.GetComponent<ORBERKILL>().lastTime)).ToString();
	}
}

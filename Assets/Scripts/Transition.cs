using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print (SceneManager.GetActiveScene().name.Equals("Title"));
	}
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
		{
			if(SceneManager.GetActiveScene().name.Equals("Title"))
				{
					SceneManager.LoadScene ("Lore");
				}
			else if(SceneManager.GetActiveScene().name.Equals("Lore"))
			{
				SceneManager.LoadScene ("Intro");
			}
			else if(SceneManager.GetActiveScene().name.Equals("Intro"))
			{
				SceneManager.LoadScene ("scene06");
			}



		}	
	}


}

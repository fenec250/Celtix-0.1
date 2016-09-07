using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject GameManager;
	// Use this for initialization
	void Awake () {
		if (GameManagerScript.instance == null)
			Instantiate (GameManager);
	
	}


}

using UnityEngine;
using System.Collections;

public class PawnFactoryScript : MonoBehaviour {

	public GameObject pawnBase;

	public static PawnFactoryScript instance = null;

	public static void instantiate(){
		GameObject pfs = new GameObject();
		pfs.name = "PawnFactory";
		pfs.AddComponent<PawnFactoryScript>();
	}

	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		pawnBase = Resources.Load<GameObject> ("Prefabs/PawnMainOdi");


	    // load all character Sprites
		/*Texture2D [] RawBodyM = (Texture2D)Resources.LoadAll("Sprites/Generic/BodyM");
		Texture2D [] RawBodyF = (Texture2D)Resources.LoadAll("Sprites/Generic/BodyF");
		Texture2D [] RawHairM = (Texture2D)Resources.LoadAll("Sprites/Generic/HairM");
		Texture2D [] RawHairF = (Texture2D)Resources.LoadAll("Sprites/Generic/HairF");
		Texture2D [] RawEyes = (Texture2D)Resources.LoadAll("Sprites/Generic/Eyes");/**/
	}

	public PawnScript getNewPawn()
	{
		return Instantiate (pawnBase).GetComponent<PawnScript>();
	}
}
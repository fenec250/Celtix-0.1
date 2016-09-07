using UnityEngine;
using System.Collections;

public class PawnScript : MonoBehaviour {
	public delegate void pawnAction(PawnScript p);
	public static pawnAction inspectPawn = delegate(PawnScript p){
		p.inspect();
	};

	private string pawnName = "Odivallus";
	private Sprite profile = null;
	private float lifespan = 1000f;
	private Animator animator;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();
		//lifespan = 666f; // debug
		//inspect ();//debug


	}
	
	// Update is called once per frame
	private bool isInGameManager = false;
	void Update () {
		if (!isInGameManager && GameManagerScript.instance != null) {
			GameManagerScript.instance.addPawn(this);
			isInGameManager = true;
		}
	}

	// displays this Pawn's infos and options in the side menu
	public void inspect(){
		// displays informations
		SideMenuScript.instance.display (pawnName, profile, lifespan/1000f);

		// generates options list to move to any existing POI.
		foreach (AreaScript area in GameManagerScript.instance.areaList){
			foreach (POIScript poi in area.poiList) {
				POIScript localPoi = poi; // need to make a local pointer to the POI or else
										  // all options move the worker to the last POI in the list
				SideMenuScript.instance.addOption (delegate {
					localPoi.addWorker (this);
				},
				localPoi.name);
			}
		}
	}
	/// <summary>
	/// Removes the Pawn from its current POI.
	/// </summary>
	/// <returns><c>true</c>, if POI was exited, <c>false</c> otherwise.</returns>
	public bool changePOI(){
		if (transform.parent == null || 
		    transform.parent.GetComponent<POIScript>() == null)
			return true;
		POIScript currentPOI = transform.parent.GetComponent<POIScript> ();

		return currentPOI.removeWorker (this);
	}

	public float age(float amount){
		lifespan -= amount;
		if (lifespan <= 0)
			GameManagerScript.instance.pawnDie (this);

		return lifespan;
	}

	public void hide(){
		animator.SetBool ("hidden", true);
	}
	
	public void show(){
		animator.SetBool ("hidden", false);
	}


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance = null;

	public int updateClock = 0;
	public int turnLenght = 20; //set in the Unity Inspector for debugging. // 100 = 1 sec I think.
	public Vector3 pawnScale = new Vector3 (10f, 10f, 1);

	public GameObject PawnFactory;

	private PawnFactoryScript factory;
	public List<PawnScript> pawnList, deadPawnList;
	//public List<GameRessource> RessList;
	public List<AreaScript> areaList;
	public AreaScript currentArea;
	// Use this for initialization  
	void Awake () {

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		if (PawnFactoryScript.instance == null)
			PawnFactoryScript.instantiate ();

	}

	// Update is called once per frame
	void Update () {
		if (updateClock < turnLenght)
			updateClock++;
		else
			turnUpdate ();

		if (updateClock % 10 == 0) // temporary
			GameResource.updateDisplay ();
	}

	private void turnUpdate(){
		updateClock = 0;

		// Destroy Pawns that died this turn.
		foreach (PawnScript pawn in deadPawnList) {
			pawn.changePOI();
			pawnList.Remove(pawn);
			Destroy(pawn.gameObject);
		}
		deadPawnList.Clear ();
	}

	void setUpdateList(){
		int totalUpdate = 0;
		foreach (AreaScript area in areaList){
			totalUpdate = area.setUpdateList(totalUpdate);
		}
	}
	public void addArea(AreaScript area){
		areaList.Add (area);
		setUpdateList ();
	}
	public void addPawn(PawnScript pawn){
		pawnList.Add (pawn);
		setUpdateList ();
	}
	public void pawnDie(PawnScript pawn){
		deadPawnList.Add (pawn);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaScript : MonoBehaviour {

	public List<POIScript> poiList;
	public int debugListCoount;

	// Use this for initialization
	void Awake () {
		poiList = new List<POIScript>( GetComponentsInChildren<POIScript> ());
	}

	private int updateOffset = 0;
	// Update is called once per frame
	private bool isInGameManager = false;
	public void Update () {
		if (!isInGameManager && GameManagerScript.instance != null) {
			GameManagerScript.instance.addArea(this);
			isInGameManager = true;
		}

		if (GameManagerScript.instance.updateClock == updateOffset)
			turnUpdate ();
	}

	private void turnUpdate(){
		debugListCoount = poiList.Count;
	}

	public int setUpdateList(int offset){
		updateOffset = offset++;
		foreach (POIScript poi in poiList) {
			offset = poi.setUpdateList(offset);
		}
		return offset;
	}

	public void inspect(){
		SideMenuScript.instance.display ("areaName", null);
	}
}

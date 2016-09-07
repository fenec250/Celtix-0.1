using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/*
 * 
 *        	was moved to GameResource as static (singleton)
 * 			keeping the dead code for reference, just in case.
 * 
 * 
public class ResourceManager : MonoBehaviour {//To be updated once Resources is done.

	public static ResourceManager instance = null;
	
	public static void instantiate(){
		GameObject pfs = new GameObject();
		pfs.name = "RessourceManager";
		pfs.AddComponent<ResourceManager>();
	}

	private Dictionary<string, GameResource> ressourceList;
	public GameObject singleRessource;
	
	void Awake () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		ressourceList = new Dictionary<string, GameResource> ();
		singleRessource = Resources.Load<GameObject>("Prefabs/SingleRessource");
		
	}

	public void updateDisplay(){
		foreach (GameResource ress in ressourceList.Values) {
			ress.update ();
		}
	}
	public void create (string name, int maximum = 0, Sprite icon = null){

		if (name == "")
			return;

		GameResource ress;
		if (ressourceList.TryGetValue (name, out ress))
			return;
		// else
		//GameObject display = Instantiate (singleRessource);
		//display.transform.SetParent (this.transform);
		//display.transform.localScale = transform.localScale;
		ress = new GameResource (name);

		ressourceList.Add (name, ress);
	}

	public void setVisibility (string name, bool visible){
		GameResource temp;
		ressourceList.TryGetValue (name, out temp);
		temp.visible_ = visible;
	}

	// tests the resource and returns the lack/overflow.
	public float dryChangeAmount(string name, float change){
		GameResource ress;
		if (ressourceList.TryGetValue (name, out ress) == false)
			return change;
		float amount = ress.amount_;
		amount += change;
		if (amount < 0) {
			return amount;
		}
		if (amount > ress.capacityMax_) {
			amount = amount - ress.capacityMax_;
			return amount;
		}
		// else
		return 0f;
	}
	
	public void changeAmount(string name, float change){
		GameResource ress;
		if (ressourceList.TryGetValue (name, out ress) == true)
			ress.amount_ += change;
	}

	public bool changeMax(string name, int change){
		GameResource temp;
		if (ressourceList.TryGetValue (name, out temp) == false)
			return false;
		temp.capacityMax_ += change;
		if (temp.capacityMax_ >= 0)
			return true;
		// else
		temp.capacityMax_ -= change;
		return false;
	}


}*/
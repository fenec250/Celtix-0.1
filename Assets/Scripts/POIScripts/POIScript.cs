using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[XmlRoot("POIRoot")]
public class POIScript : MonoBehaviour {
	protected const float agingIdle = 1f;

	public List<PawnScript> workerList_;//{get{ return workerList_; }}
	protected int maxWorkers_ = -1; 
	//protected int rank_ = 1;

	public POIScript(){
		workerList_ = new List<PawnScript>();
	}
	
	private int updateOffset = 0;// personnal offset for turnUpdate

	/// <summary>
	/// If it's my turn, compute my turn using turnUpdate.
	/// </summary>
	void Update () {
		if (GameManagerScript.instance.updateClock == updateOffset)
			turnUpdate ();
	}

	/// <summary>
	/// Compute this POI's turn.
	/// </summary>
	protected virtual void turnUpdate(){

	}

	/// <summary>
	/// accept our turn in the Cycle.
	/// </summary>
	/// <returns>The next turn.</returns>
	/// <param name="offset">Our turn.</param>
	public int setUpdateList(int offset){
		updateOffset = ++offset;
		return offset;
	}

	// try and move the worker to this POI
	/// <summary>
	/// Try and add the worker to this POI.
	/// </summary>
	/// <param name="worker">The worker to add to the POI</param>
	public void addWorker(PawnScript worker){
		if (workerList_.Count >= maxWorkers_)
			return;
		if (worker.changePOI ()) {
			workerList_.Add (worker);//
			worker.transform.SetParent (transform);
			worker.transform.localScale = transform.localScale;
			// not fully auto-adjusting yet
			placeWorkers();
		}
	}

	// temporary, for using addWorker from Unity Inspector.
	public void addWorker(GameObject worker){ 
		addWorker (worker.GetComponent<PawnScript> ());

	}

	// searches and removes the worker from the internal workerlist
	// Does NOT move/unparent the worker's gameObject
	public bool removeWorker(PawnScript worker){
		return workerList_.Remove (worker);
	}

	// Places Workers in front of the POI
	public void placeWorkers(){
		for (int i = 0; i < workerList_.Count; ++i){
			workerList_[i].transform.localPosition = new Vector3((float)(i%5-2f)*10f,
			                                                    (float)((i-1)/5-workerList_.Count/10)*10f);
			workerList_[i].transform.SetAsFirstSibling();
		
		}
		transform.Find ("Button").SetAsFirstSibling ();
	}
	/// <summary>
	/// displays basic informations about this POI
	/// </summary>
	public virtual void inspect(){
		SideMenuScript.instance.display (gameObject.name, 
		                                 transform.FindChild("appearance").GetComponent<SpriteRenderer> ().sprite, 
		                                 (float)workerList_.Count / (float)maxWorkers_);
	}

	/// <summary>
	/// Presents all characters
	/// </summary>
	/// <param name="action">Action.</param>
	public void selectWorker(PawnScript.pawnAction action){
		if (workerList_.Count > 0)
			SideMenuScript.instance.clear();
		for (int i = 0; i < workerList_.Count; ++i) {
			int id = i;
			SideMenuScript.instance.addOption (delegate {
				action(workerList_[id]);
			}, workerList_[i].name);
		}

	}
	/*// asks the player if it wants to upgrade the POI
	public virtual void upgradeAsk(){
		MenuWindowScript.instance.show ("Do you want to upgrade " + gameObject.name + " for " + "(debug:free)" + "?");
		MenuWindowScript.instance.addOption (delegate {
			upgrade();
			MenuWindowScript.instance.close();
		}, "Yes");
		MenuWindowScript.instance.addOption (delegate {
			MenuWindowScript.instance.close ();
		});
		
	}
	protected virtual bool upgrade(bool free = false, int nbRankUp = 1){
		if (!free) {
			// try to pay here, return false if can't
		}
		rank_ += nbRankUp;
		maxWorkers_ = rank_ * 2;
		return true;
	}*/
}
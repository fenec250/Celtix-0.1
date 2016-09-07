using UnityEngine;
using System.Collections;

public class Fields_poi : POIScript
{
	private const string foodResourceName = "Food";
	private GameResource foodResource_;
	private const string pathSpritePOI = "Field";// TODO: change the path and image location.
	private const int maxInitial = 100;
	private const float agingPerActiveCycle = 2.5f;
	private int rank_;
	//public Sprite icon; // temp, set the icon with Unity Inspector. TODO: load its icon itself.
	// Use this for initialization
	void Start ()
	{
		foodResource_ = GameResource.getGameResource(foodResourceName);
		foodResource_.addCapacity (getFoodStorage);
		SpriteRenderer appearance = transform.FindChild("appearance").GetComponent<SpriteRenderer> ();
		appearance.sprite = Resources.Load<Sprite> (pathSpritePOI);
		GetComponent<RectTransform> ().sizeDelta = appearance.sprite.bounds.size;// fits 
		maxWorkers_ = 4;
		rank_ = 1;
	}

	void OnDestroy (){
		foodResource_.removeCapacity (getFoodStorage);
	}

	// Update is called once per frame
	protected override void turnUpdate ()
	{
		base.turnUpdate ();
		foreach (PawnScript worker in workerList_) {
			if (foodResource_.isFull() == false){
				worker.age( agingPerActiveCycle );
				foodResource_.changeAmount(rank_);
			}
			else
				worker.age( agingIdle );
		}
	}
	/// <summary>
	/// Gets the storage capacity for Food in this POI.
	/// </summary>
	/// <returns>The food storage capacity.</returns>
	public int getFoodStorage(){
		return maxInitial << rank_;
	}
	public override void inspect ()
	{
		base.inspect ();
		SideMenuScript.instance.addOption (delegate {
			upgradeAsk ();
		}, "Upgrade");
	}
	// asks the player if it wants to upgrade the 
	public virtual void upgradeAsk(){
		SideMenuScript.instance.clear ();

		TextWindowScript.instance.show ("Do you want to upgrade " + gameObject.name + " for " + "(debug:free)" + "?");

		SideMenuScript.instance.addOption (delegate {
			upgrade();
			TextWindowScript.instance.close();
			inspect();
		}, "Yes");
		SideMenuScript.instance.addOption (delegate {
			TextWindowScript.instance.close ();
			inspect();
		}, "No");
		
	}
	protected virtual bool upgrade(bool free = false, int nbRankUp = 1){
		if (!free) {
			// try to pay here, return false if can't
		}
		rank_ += nbRankUp;
		maxWorkers_ = rank_ * 2;
		return true;
	}
}


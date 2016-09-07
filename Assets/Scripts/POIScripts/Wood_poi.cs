using UnityEngine;
using System.Collections;

public class Wood_poi : POIScript
{
	private const string woodResourceName = "Wood";
	private GameResource woodResource_;
	private const string pathSpritePOI = "WoodShack";// TODO: change the path and image location.
	private const int maxInitial = 100;
	private const float agingPerActiveCycle = 2f; 
	private int rank_;
	//public Sprite icon; // temp, set the icon with Unity Inspector. TODO: load its icon itself.
	// Use this for initialization
	void Start ()
	{
		woodResource_ = GameResource.getGameResource(woodResourceName);
		woodResource_.addCapacity (getWoodStorage);
		SpriteRenderer appearance = transform.FindChild("appearance").GetComponent<SpriteRenderer> ();
		appearance.sprite = Resources.Load<Sprite> (pathSpritePOI);
		GetComponent<RectTransform> ().sizeDelta = appearance.sprite.bounds.size;// fits 
		maxWorkers_ = 4;
		rank_ = 1;
	}

	void OnDestroy (){
		woodResource_.removeCapacity (getWoodStorage);
	}

	// Update is called once per frame
	protected override void turnUpdate ()
	{
		base.turnUpdate ();

		foreach (PawnScript worker in workerList_) {
			if (woodResource_.isFull() == false){
				worker.age( agingPerActiveCycle );
				woodResource_.changeAmount(rank_);
			}
			else
				worker.age( agingIdle );

		}
	}
	/// <summary>
	/// Gets the storage capacity for Wood in this POI.
	/// </summary>
	/// <returns>The wood storage capacity.</returns>
	public int getWoodStorage(){
		return maxInitial << rank_;
	}
	public override void inspect ()
	{
		base.inspect ();
		SideMenuScript.instance.addOption (delegate {
			upgradeAsk ();
		}, "Upgrade");
		SideMenuScript.instance.addOption (delegate {
			selectWorker(PawnScript.inspectPawn);
		}, "Browse workers");

	}
	// asks the player if it wants to upgrade the 
	public virtual void upgradeAsk(){
		TextWindowScript.instance.show ("Do you want to upgrade " + gameObject.name + " for " + (50 << rank_).ToString() + "Wood?");
		SideMenuScript.instance.clear ();
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

			//GameResource wood = GameResource.getGameResource("Wood");
			if (!(woodResource_.getAmount() >= 50 << rank_))
				return false;

			woodResource_.changeAmount( - 50 << rank_);
		}
		rank_ += nbRankUp;

		maxWorkers_ = rank_ * 2;
		return true;
	}
}


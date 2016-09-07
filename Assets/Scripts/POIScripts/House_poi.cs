using UnityEngine;
using System.Collections;

public class House_poi : POIScript
{
	
	private const string foodResourceName = "Food";
	private GameResource foodResource_;
	private const string pathSpritePOI = "Chaumiere";// TODO: change the path and image location.
	//private const int maxInitial = 100;
	//private int rank_;

	// Use this for initialization
	void Start ()
	{
		foodResource_ = GameResource.getGameResource(foodResourceName);
		//foodResource_.addCapacity (getFoodStorage);
		SpriteRenderer appearance = transform.FindChild("appearance").GetComponent<SpriteRenderer> ();
		appearance.sprite = Resources.Load<Sprite> (pathSpritePOI);
		GetComponent<RectTransform> ().sizeDelta = appearance.sprite.bounds.size;// fits 
		maxWorkers_ = 20;
		//rank_ = 1;
	}
	
	void OnDestroy (){
		//foodResource_.removeCapacity (getFoodStorage);
	}
	
	// Update is called once per frame
	protected override void turnUpdate ()
	{
		base.turnUpdate ();
		foreach (PawnScript worker in workerList_) {
			worker.age( agingIdle );
		}
	}
	/// <summary>
	/// Gets the storage capacity for Food in this POI.
	/// </summary>
	/// <returns>The food storage capacity.</returns>
	/*public int getFoodStorage(){
		return maxInitial << rank_;
	}*/


	public override void inspect ()
	{
		base.inspect ();
		if (foodResource_.getAmount() >= costBaby())
			SideMenuScript.instance.addOption (makeBaby, "Make Baby");

		/*SideMenuScript.instance.addOption (delegate {
			upgradeAsk ();
		}, "Upgrade");*/
	}

	private void makeBaby(){
		
		PawnScript baby = PawnFactoryScript.instance.getNewPawn ();

		TextWindowScript.instance.show ("Do you want to give birth to " + baby.name + " for " + costBaby() + " Food?");
		
		SideMenuScript.instance.clear();
		SideMenuScript.instance.addOption (delegate {
			SideMenuScript.instance.clear();
			if (workerList_.Count < maxWorkers_) {
				addWorker(baby);
				foodResource_.changeAmount(-costBaby());
				TextWindowScript.instance.show (baby.name + "is born!");
			}
			else{
				TextWindowScript.instance.show ("You did not have enough Food, " + baby.name + " is stillborn!");
			}
			SideMenuScript.instance.addOption(delegate{
				
				TextWindowScript.instance.close();
				inspect();
			}, "Ok");
		}, "Yes");
		SideMenuScript.instance.addOption (delegate {
			TextWindowScript.instance.close ();
			inspect();
		}, "No");

	}

	private float costBaby(){
		return 100f;
	}

	// asks the player if it wants to upgrade the 
	/*public virtual void upgradeAsk(){
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
	}*/
}


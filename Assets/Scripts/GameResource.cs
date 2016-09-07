using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameResource { 

	//////////////////////////// STATIC (SINGLETON) PART  /////////////////
	/// 
	public delegate int getIndividualCapacity();
	private static Dictionary<string, GameResource> allGameResources = new Dictionary<string, GameResource> ();

	public static GameResource getGameResource(string resourceName){
		GameResource temp;
		if (allGameResources.TryGetValue (resourceName, out temp)) 
			return temp;
	//  else
		temp = new GameResource (resourceName);// TODO: make this constructor.
		allGameResources.Add (resourceName, temp);

		return temp;
	}

	public static void cleanUpGameResources(){
		foreach (KeyValuePair<string, GameResource> res in allGameResources) {
			if (res.Value.refreshCapacityTotal () == 0)
				allGameResources.Remove (res.Key);
		}
	}

	public static void updateDisplay (){
		foreach (GameResource ress in allGameResources.Values) {
			ress.update ();
		}
	}

	/////////////////////////// CAPACITY MANIPULATION //////////////////////

	/// <summary>
	/// The capacity max_.
	/// </summary>
	private int capacityMax_;
	private List<getIndividualCapacity> listSubCapacityMaxFunc_;

	// adds a new resource container.
	/// <summary>
	/// Adds a function reference to the list of functions for maximum resource capacity.
	/// </summary>
	/// <returns><c>false</c>, if maximum was already added, <c>false</c> otherwise.</returns>
	/// <param name="yourFunction">Function returning the capacity of the caller for this resource</param>
	public bool addCapacity( getIndividualCapacity yourFunction){
		bool wasAlreadyAdded = listSubCapacityMaxFunc_.Remove (yourFunction);
		listSubCapacityMaxFunc_.Add (yourFunction);
		refreshCapacityTotal ();
		return (!wasAlreadyAdded);
	}

	/// <summary>
	/// Removes the function from the list, reducing maximum capacity..
	/// </summary>
	/// <returns><c>true</c>, if the function was found and removed, <c>false</c> otherwise.</returns>
	/// <param name="yourFunction">Your function.</param>
	public bool removeCapacity( getIndividualCapacity yourFunction){
		bool succesfullyRemoved = listSubCapacityMaxFunc_.Remove (yourFunction);
		refreshCapacityTotal ();
		return succesfullyRemoved;
	}

	// recalculates the total maximum capacity for this Resource.
	private int refreshCapacityTotal(){
		int totalCapacity = 0;
		foreach (getIndividualCapacity func in listSubCapacityMaxFunc_) {
			totalCapacity += func();
		}
		capacityMax_ = totalCapacity;
		return totalCapacity;
	}
	public int getCapacityTotal(){
		return capacityMax_;
	}







	private string name_;
	private float amount_;
	private bool visible_;
	private GameObject display_;
	
	public GameResource(string name, string AssetsPath = ""){
		this.name_ = name;
		amount_ = 0f;
		visible_ = true;
		display_ = GameObject.Instantiate ( Resources.Load<GameObject>("Prefabs/SingleResource"));
		GameObject ResourceHolder = GameObject.Find ("ResourceHolder");
		display_.transform.SetParent (ResourceHolder.transform);
		display_.transform.localScale = ResourceHolder.transform.localScale;
		display_.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(AssetsPath + name + "Icon");

		display_.SetActive (visible_);
		if (visible_)
			display_.GetComponentInChildren<Text> ().text = amount_.ToString ();

		listSubCapacityMaxFunc_ = new List<getIndividualCapacity> ();
	}
	
	public void update(){
		display_.SetActive (visible_);
		refreshCapacityTotal ();
		if (visible_)
			display_.GetComponentInChildren<Text> ().text = amount_.ToString ();
	}
	/// <summary>
	/// Changes the amount of this resource stock. Can go below 0 and over the maximum; Test it yourself with isFilled() and isEmpty().
	/// </summary>
	/// <param name="amount">Value to add to the amount. Negative value reduces the amount.</param>
	public void changeAmount( float amount){
		amount_ += amount;
	}
	public float getAmount(){
		return amount_;
	}
	public bool isFull(){
		return amount_ >= capacityMax_;
	}
	public bool isEmpty(){
		return amount_ <= 0;
	}

	public void setVisibility (bool visible){
		visible_ = visible;
	}
}

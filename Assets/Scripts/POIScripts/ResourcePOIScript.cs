using UnityEngine;
using System.Collections;

// being reworked. many things used here don't work anymore.
// kept for reference.

/*public class ResourcePOIScript : POIScript{
	// set these in the Ispector
	public string resource = "Resource";
	public Sprite icon;
	public int maximum;

	protected float workRate = 1f, workAmount = 5f;
	protected int resourceBuffer;
	
	// Use this for initialization
	protected void Start () {
		ResourceManager.instance.create (resource, maximum, icon);
		upgrade (true);// starts rank 1
		//Vector3 spriteSize = GetComponent<SpriteRenderer> ().sprite.bounds.size;
		//GetComponent<RectTransform>().sizeDelta = new Vector2(spriteSize.x, spriteSize.y);
		GetComponent<RectTransform> ().sizeDelta = GetComponent<SpriteRenderer> ().sprite.bounds.size;
	}
	
	// generates resources by making the workers work.
	protected override void turnUpdate ()
	{
		if (resourceBuffer != 0) { // if there was exceeding ressources last turn, halt production and try to turn in the extra ressources
			resourceBuffer = ResourceManager.instance.changeAmount (resource, resourceBuffer);// stores any exceeding ressources in the buffer
			foreach (PawnScript worker in workerList_)
				worker.work (1f); // makes workers age 
		} 
		else {// if 
			float resourceGain = 0f;
			foreach (PawnScript worker in workerList_) {
				resourceGain += workRate * worker.work (workAmount);
			}
			resourceBuffer = ResourceManager.instance.changeAmount (resource, (int)resourceGain);// stores any exceeding ressources in the buffer
		}
	}
	
	public override void inspect ()
	{
		base.inspect ();
		SideMenuScript.instance.addOption(delegate {upgradeAsk();}, "Upgrade");
	}
};

public abstract class POIAction{
	protected string type{ get; };

	public POIAction(){
		type = "POIAction";
	}

	public virtual bool execute();
	public virtual void undo ();

}
public class POIActionFlatRessourceChange:POIAction{
	public string resource = "Resource";
	public Sprite icon;
	public int maximum;

	private int amount;
	private bool paidFor;

	public POIActionFlatRessourceChange(){
		type = 
		ResourceManager.instance.create (resource, maximum, icon);
	}

	override bool execute(){
		if (paidFor) {
			paidFor = false;
			return true;
		}
		if (ResourceManager.instance.changeAmount (resource, amount) == 0) {
			return true;
		}
		// else
		ResourceManager.instance.changeAmount (resource, amount);// refund
		return false;
	}

	override void undo ()
	{
		// throw an axception if already paidFor?
		paidFor = true;
	}
}*/
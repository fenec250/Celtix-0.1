using UnityEngine;
using System.Collections;





// probably obsolete, not needed anymore





public abstract class ButtonOperation : MonoBehaviour
{
	public abstract void operation ();
}

public class ButtonOperator : MonoBehaviour
{
	// may not be necessary if you can use polymorphism in the Inspector.
	private BOMovePawn bop;

	//debug
	public GameObject pawn;
	public GameObject poi;
	void Awake(){
		bop = new BOMovePawn ();
		bop.set (pawn.GetComponent<PawnScript> (), poi.GetComponent<POIScript> ());
	}

	public void operation(){
		bop.operation ();
	}
}

public class BOMovePawn : ButtonOperation
{
	public PawnScript pawn;
	public POIScript poi;

	public override void operation (){
		poi.addWorker (pawn);
	}
	
	public void operation (PawnScript fpawn, POIScript fpoi){
		fpoi.addWorker (fpawn);
	}

	public void set(PawnScript newPawn, POIScript newPoi){
		poi = newPoi;
		pawn = newPawn;
	}
}

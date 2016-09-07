using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public GameObject _optionPanel; 

	public void testAddOption(GameObject optionPanel, GameObject poi){
		optionPanel.GetComponent<OptionPanelScript> ().addOption (delegate{poi.GetComponent<POIScript> ().addWorker (PawnFactoryScript.instance.getNewPawn ());});
	}
	
	public void testAddOption(GameObject poi){
		_optionPanel.GetComponent<OptionPanelScript> ().addOption (delegate{poi.GetComponent<POIScript> ().addWorker (PawnFactoryScript.instance.getNewPawn ());});
	}
	public void testAddPawn(GameObject poi){
		poi.GetComponent<POIScript>().addWorker(PawnFactoryScript.instance.getNewPawn ());

	}
	public void testInfoPanel(GameObject infoPanel){
		InfoPanelScript script = infoPanel.GetComponent<InfoPanelScript> ();
		script.clear ();
		script.addText ("Test Info Panel");
		script.addSlider (33f, 100f);
	}

	public void test2(GameObject pawn, GameObject poi, GameObject button){//
		BOMovePawn bop = button.AddComponent<BOMovePawn> ();
		button.GetComponent<Button> ().onClick.AddListener (delegate {
			bop.operation (pawn.GetComponent<PawnScript> (), poi.GetComponent<POIScript> ()); });
	}

	private int nbRessource = 0;
	public Sprite icon;
	public void testAddRessource(){
		++nbRessource;
		GameResource tempRes = GameResource.getGameResource (nbRessource.ToString ());

		tempRes.addCapacity (maxResourceForTestingMaxResource);
		tempRes.addCapacity (maxResourceForTestingMaxResource);

		tempRes.addCapacity (secondMaxResourceForTestingMaxResource);

		tempRes.removeCapacity (secondMaxResourceForTestingMaxResource);

		tempRes.changeAmount (nbRessource % tempRes.getCapacityTotal());

		/*if (nbRessource % 2 == 1)
			tempRes.setVisibility (false);*/
	}
	private int maxResourceForTestingMaxResource(){
		return 2;
	}
	private int secondMaxResourceForTestingMaxResource(){
		return 4;
	}

	/*public void testCreateActionXML(){
		ActionBase.CreateArbitraryPOIFile ();
	}*/

	/// <summary>
	/// flag for searching within Monodevelop. 
	/// Use wherever you put temp code for debugging. 
	/// Search reference to this function to retrieve where you put the code.
	/// </summary>
	public static void debugFlag(){
		return;
	}
}


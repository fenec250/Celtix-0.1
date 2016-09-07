using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SideMenuScript : MonoBehaviour {

	private GameObject currentElement;
	private Image profileImage;
	//private Text currentName;
	//private Slider lifeSlider;
	public OptionPanelScript optionPanel;
	public InfoPanelScript infoPanel;
	
	public static SideMenuScript instance = null;
	
	// Use this for initialization
	void Awake () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);


		profileImage = transform.Find ("ProfileImage").GetComponent<Image> ();
		//profileImage.enabled = false;

		//currentName = transform.Find ("Name").GetComponent<Text> ();
		//currentName.text = "";
		//lifeSlider = transform.GetComponentInChildren<Slider> ();
		//lifeSlider = transform.Find ("Slider").GetComponent<Slider> ();
		//lifeSlider.value = 0f;

		optionPanel = transform.Find ("OptionPanel").GetComponent<OptionPanelScript> ();
		infoPanel = transform.Find ("InfoPanel").GetComponent<InfoPanelScript> ();
	}
	// Update is called once per frame
	void Update () {
	}

	public void clear(){
		//currentName.text = "";
		profileImage.enabled = false;
		//lifeSlider.enabled = false;
		optionPanel.clear ();
		infoPanel.clear ();
	}

	public void display( string name, Sprite profile = null, float lifespan = -1f)
	{
		optionPanel.clear ();
		infoPanel.clear ();

		if (profile == null)
			profileImage.enabled = false;
		else { 
			profileImage.enabled = true;
			profileImage.sprite = profile;
		}

		infoPanel.addText (name);

		if (lifespan != -1f)
			infoPanel.addSlider (lifespan, 1f);

		/*if (lifespan == -1f)
			lifeSlider.gameObject.SetActive(false);
		else {
			lifeSlider.value = lifespan;
			lifeSlider.gameObject.SetActive(true);
		}*/
		//currentName.text = name;
	}
	public void addOption( UnityEngine.Events.UnityAction action, string text = ""){
		optionPanel.addOption (action, text);
	}
	// temp debug
	/*public void testAddOption(GameObject poi){
		addOption (delegate{poi.GetComponent<POIScript> ().addWorker (PawnFactoryScript.instance.getNewPawn ());});
	}*/

}

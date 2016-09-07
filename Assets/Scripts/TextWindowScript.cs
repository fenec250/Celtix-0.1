using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextWindowScript : MonoBehaviour {
	
	public static TextWindowScript instance = null;
	public GameObject window;
	private Text mainText;
	//private OptionPanelScript options;

	public static void instantiate(){
		GameObject pfs = new GameObject();
		pfs.name = "MenuWindowManager";
		pfs.AddComponent<TextWindowScript>();
	}
	
	private GameObject windowPrefab;
	
	void Awake () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		//windowPrefab = Resources.Load<GameObject> ("Prefabs/PopUpWindow");
		window.SetActive (true);
		mainText = window.transform.Find("TextView").Find("mainText").gameObject.GetComponent<Text> ();
		//options = window.GetComponentInChildren<OptionPanelScript> ();
		window.SetActive (false);
	}
	
	public void updateDisplay(){
		
	}

	public void show(string text = ""){
		window.SetActive (true);
		mainText.text = text;
		//options.clear ();
	}
	
	public void addText(string text = "\n"){
		window.SetActive (true);
		mainText.text += text;
	}

	/*public void addOption( UnityEngine.Events.UnityAction action, string text = ""){
		window.SetActive (true);
		options.addOption (action, text);
	}*/
	public void hide(){
		window.SetActive (false);
	}
	public void close(){
		mainText.text = "";
		//options.clear ();
		window.SetActive (false);
	}
}

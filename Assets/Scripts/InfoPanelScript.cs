using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InfoPanelScript : MonoBehaviour {

	private const int FONTSIZE = 12;

	public GameObject sliderPrefab;
	public Text textPrefab;
	//private List<UnityEngine.Events.UnityAction> actionList;

	private Transform infoHolder;
	// Use this for initialization
	void Awake () {
		infoHolder = transform.Find ("InfoHolder");
		//actionList = new List<UnityEngine.Events.UnityAction>();
	}

	/*public void addOption( UnityEngine.Events.UnityAction action, string text = ""){
		GameObject newOption = Instantiate<GameObject> (singleOptionPrefab);
		actionList.Add (action);
		//newOption.GetComponentInChildren<Button> ().onClick.AddListener (action);
		newOption.transform.SetParent (optionHolder);
		newOption.transform.localScale = new Vector3 (1f, 1f, 1f);
		newOption.GetComponentInChildren<Text> ().text = text;
	}*/
	public void addText( string text ){
		Text textObj = Text.Instantiate(textPrefab);
		textObj.fontSize = FONTSIZE;
		textObj.text = text;
		textObj.transform.SetParent (infoHolder);
		textObj.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	
	public void addSlider( float position, float max ){
		GameObject sliderObj = Instantiate<GameObject>(sliderPrefab);
		Slider slider  = sliderObj.transform.Find ("Slider").GetComponent<Slider> ();
		slider.value = position / max;
		sliderObj.transform.SetParent (infoHolder);
		sliderObj.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
	
	public void addSlider( List<float> values ){
		if (values.Count <= 0)
			return;
		if (values.Count == 1) {
			addSlider (values [0], values [0]);
			return;
		}
		values.Sort ();
		if (values.Count == 2) {
			addSlider (values [1], values [0]);
			return;
		}
		GameObject sliderObj = Instantiate<GameObject>(sliderPrefab);
		Slider slider  = sliderObj.transform.Find ("Slider").GetComponent<Slider> ();
		//slider.value = position / max;
	}

	public void clear(){
		foreach (Transform child in infoHolder)
			Destroy (child.gameObject);
	}

}

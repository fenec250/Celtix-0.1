using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OptionPanelScript : MonoBehaviour {

	public GameObject singleOptionPrefab;
	private List<UnityEngine.Events.UnityAction> actionList;

	private Transform optionHolder;
	// Use this for initialization
	void Awake () {
		optionHolder = transform.Find ("OptionHolder");
		//actionList = new List<UnityEngine.Events.UnityAction>();
	}
	/// <summary>
	/// Adds a button to the Option menu. When pressed it executes the provided action.
	/// </summary>
	/// <param name="action">Action to be executed when the button is pressed.</param>
	/// <param name="text">Text to display on the button.</param>
	public void addOption( UnityEngine.Events.UnityAction action, string text = ""){
		GameObject newOption = Instantiate<GameObject> (singleOptionPrefab);
		//actionList.Add (action);
		newOption.GetComponentInChildren<Button> ().onClick.AddListener (action);//(actionList[actionList.Count -1]);
		newOption.transform.SetParent (optionHolder);
		newOption.transform.localScale = new Vector3 (1f, 1f, 1f);
		newOption.GetComponentInChildren<Text> ().text = text;
	}

	public void clear(){
		foreach (Transform child in optionHolder)
			Destroy (child.gameObject);
		//actionList.Clear ();
	}

}

using UnityEngine;
using System.Collections;
using System.Xml;

public class POI_XML : POIScript
{
	public string filename;
	public ActionGroup actionGroup_;

	// Use this for initialization
	void Start ()
	{
		
		actionGroup_ = new ActionGroup ();
		actionGroup_.linkPOI (this);

		System.Console.WriteLine ("XML start");
		//      Load
	    System.Xml.XmlDocument XMLDoc;
	    XMLDoc = new System.Xml.XmlDocument();
	    try
	    {
	        XMLDoc.Load(filename);
	    }
	    catch (System.Exception)
		{
			System.Console.WriteLine ("XML error loading file");
			//nouvelleConfig();// // TODO: change that, it will reset the profile and keybinds (but they might be set after).
			Destroy(gameObject);
			return;
	    }
	    try
	    {
			foreach( XmlNode node in XMLDoc.DocumentElement.ChildNodes)//.SelectSingleNode("ActionGroup");
			{
				ActionBase action;
				extractAction(node, out action);
				//actionGroup_.mergeAction
			}
		}catch (System.Exception)
	    {
	        //nouvelleConfig();// TODO: change that, it will reset the profile and keybinds (but they might be set after).
			Destroy(gameObject);
			return;
	    }
	}
	/*     Saving
	System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
	System.Xml.XmlNode rootNode = xmlDoc.CreateElement(fichier.Replace(' ', '_'));
	xmlDoc.AppendChild(rootNode);
	
	System.Xml.XmlNode robotConfig = xmlDoc.CreateElement("robotConfig");
    System.Xml.XmlAttribute attribute = xmlDoc.CreateAttribute("robotName");
    attribute.Value = currentProfileDropDown.Text;
    robotConfig.Attributes.Append(attribute);
	*/


	
	// Update is called once per frame
	void Update ()
	{
		if (actionGroup_.dryRun (1))
			actionGroup_.execute (1);
	}
	
	
	public void addAction( ActionBase action){
		actionGroup_.mergeAction (action);
	}

	private bool extractAction( XmlNode node, out ActionBase action){
		string temp = "Name: " + node.Name + "\nLocalName: " + node.LocalName;
		switch (node.Name) {
		default:
		case "ActionBase":
		case "ActionGroup"://TODO: fill that
		case "ActionLifespan":
		case "ActionResource":
		case "ActionPerWorker":
			TextWindowScript.instance.show (temp);
			action = new ActionGroup ();
			break;

		}
		return false;
		
		//interrupteurDebug = debugConfig.Attributes.GetNamedItem("interrupteurDebug").Value == "1";
		//if (interrupteurDebug) toggleBigButtonOn(); else toggleBigButtonOff();
		
		//Int32.Parse(keybinds.Attributes.GetNamedItem("forwardsKey").Value),
	}
}


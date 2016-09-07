using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

// base for any POI action
// might be of use later: http://stackoverflow.com/questions/27391485/invalidoperationexception-the-type-of-the-argument-object-scratch-is-not-prim
/*[XmlInclude(typeof(ActionLifespan))]
[XmlInclude(typeof(ActionResource))]
[XmlInclude(typeof(ActionGroup))]
[XmlInclude(typeof(ActionPerWorker))]*/
//[Serializable]
public abstract class ActionBase
{

	//             Obsolete, to be updated.



	public const string XmlName = "ActionBase";
	//[System.NonSerialized]
	// link to the POI using this action
	protected POI_XML parentPOI_;
	
	public ActionBase(){parentPOI_ = null;}

	// determines wheter this action can be executed.
	public abstract bool dryRun (float multiplier);
	// applies this action
	public abstract void execute (float multiplier);
	//sets the parentPOI
	public virtual void linkPOI(POI_XML parentPOI){parentPOI_ = parentPOI;}
	// returns this node in XML format
	public abstract XmlNode saveXML (XmlDocument xmlDoc);
	// Tries to merge the action with this action
	public abstract bool mergeAction (ActionBase newAction);
	// permanently multiplies this action's attributes by the multiplier, for use in composite mergeAction
	//public abstract void scale (float multiplicator){ throw new Exception ("ActionBase scaling");}
	
	public static void CreateArbitraryPOIFile(){
		//var serializer = new XmlSerializer(typeof(ActionBase), GetAllSubTypes(typeof(ActionBase)));
		var stream = new FileStream( Application.persistentDataPath + "//testXML2", FileMode.Create);
		GameObject poi = new GameObject();
		poi.name = "arbitraryPOIname";
		POI_XML temp = poi.AddComponent<POI_XML>();
		List<ActionBase> list = new List<ActionBase> ();
		list.Add (new ActionResource ( "testRes", 4.20f));
		list.Add (new ActionLifespan ( 2.10f));
		ActionPerWorker temp2 = new ActionPerWorker (new ActionGroup (list));
		temp.addAction( temp2);
		//serializer.Serialize(stream, temp2);
		//stream.Close();
		
		System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
		System.Xml.XmlNode rootNode = xmlDoc.CreateElement("testXML2");
		xmlDoc.AppendChild(rootNode);
		rootNode.AppendChild (temp2.saveXML (xmlDoc));
		xmlDoc.Save ("testXML2");

	}
	
	protected static bool extractAction( XmlNode node, out ActionBase action){
		string temp = "Name: " + node.Name + "\nLocalName: " + node.LocalName;
		switch (node.Name) {
		case "ActionGroup"://TODO: fill that
			action = new ActionGroup(node);
			return true;
		case "ActionLifespan":
			action = new ActionLifespan(node);
			return true;
		case "ActionResource":
			action = new ActionResource(node);
			return true;
		case "ActionPerWorker":
			action = new ActionPerWorker(node);
			return true;
		default:
		case "ActionBase":
			TextWindowScript.instance.show (temp);
			action = new ActionGroup ();
			break;
			
		}
		return false;
		
		//interrupteurDebug = debugConfig.Attributes.GetNamedItem("interrupteurDebug").Value == "1";
		//if (interrupteurDebug) toggleBigButtonOn(); else toggleBigButtonOff();
		
		//Int32.Parse(keybinds.Attributes.GetNamedItem("forwardsKey").Value),
	}
	/*
	public static Type[] GetAllSubTypes(Type aBaseClass)
	{
		List<Type> result = new List<Type>();
		Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly a in assemblies)
		{
			Type[] types = a.GetTypes();
			foreach (Type t in types)
			{
				if (t.IsSubclassOf(aBaseClass))
					result.Add(t);
			}
		}
		return result.ToArray();
	}*/
} 


public class ActionLifespan : ActionBase{
	//[SerializeField]
	public new const string XmlName = "ActionLifespan";
	protected float amount_;
	
	public ActionLifespan(){amount_= 0f;}
	public ActionLifespan(float amount ){amount_ = amount;}
	public ActionLifespan(XmlNode node){
		if (node.Name != XmlName){
			throw(new Exception ("bad ActionLifespan Xml"));
			return;
		}
		amount_ = float.Parse (node.Attributes.GetNamedItem ("amount").Value);
	}

	public override bool dryRun(float multiplier){ 
		return (parentPOI_.workerList_.Count > 0);
	}

	public override void execute(float multiplier){
		float amountPerWorker = amount_ * multiplier / parentPOI_.workerList_.Count;
		foreach (PawnScript worker in parentPOI_.workerList_) {
			worker.age(amountPerWorker);
		}
	}
	
	public override XmlNode saveXML(XmlDocument xmlDoc){
			XmlNode node = xmlDoc.CreateElement(XmlName);
		XmlAttribute attribute = xmlDoc.CreateAttribute("amount_");
		attribute.Value = amount_.ToString();
		node.Attributes.Append(attribute);
		return node;
	}
	
	public override bool mergeAction(ActionBase newAction){
		if (newAction.GetType () == this.GetType ()) {
			amount_ += ((ActionLifespan)newAction).amount_;
			return true;
		}
		return false;
	}

	/*public override void scale (float multiplicator){
		amount_ *= multiplicator;
	}*/
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

public class ActionResource : ActionBase{
	public new const string XmlName = "ActionResource";
	//[XmlAttribute("ActionResource")]
	//[SerializeField]
	protected string resourceName_;//{ get{return resource_;} }
	protected GameResource resource_;
	//[SerializeField]
	protected float amount_;//{ get{ return amount_; } }
	
	public ActionResource(){
		resourceName_ = "";
		amount_ = 0f;
	}
	public ActionResource(string resourceName, float amount){
		resourceName_ = resourceName;
		resource_ = GameResource.getGameResource (resourceName_);
		amount_ = amount;
	}
	public ActionResource(XmlNode node){
		if (node.Name != XmlName){
			throw(new Exception ("bad ActionResource Xml"));
			return;
		}
		amount_ = float.Parse (node.Attributes.GetNamedItem ("amount").Value);
		resourceName_ = node.Attributes.GetNamedItem ("amount").Value;
		resource_ = GameResource.getGameResource (resourceName_);
	}
	
	public override bool dryRun(float multiplier){ 
		// 
		return true;//( 0f == ResourceManager.instance.dryChangeAmount (resourceName_, amount_ * multiplier));
	}
	
	public override void execute(float multiplier){
		resource_.changeAmount ( amount_ * multiplier);
	}

	public override XmlNode saveXML(XmlDocument xmlDoc){
		XmlNode node = xmlDoc.CreateElement(XmlName);

		XmlAttribute attribute = xmlDoc.CreateAttribute("resource_");
		attribute.Value = resourceName_;
		node.Attributes.Append(attribute);

		attribute = xmlDoc.CreateAttribute("amount_");
		attribute.Value = amount_.ToString();
		node.Attributes.Append(attribute);

		return node;
	}
	
	public override bool mergeAction(ActionBase newAction){
		if (newAction.GetType () == this.GetType ()) {
			ActionResource actionRes = (ActionResource)newAction;
			if (actionRes.resourceName_ == resourceName_){
				amount_ += actionRes.amount_;
				return true;
			}
		}
		return false;
	}
	
	/*public override void scale (float multiplicator){
		amount_ *= multiplicator;
	}*/
}


public class ActionGroup : ActionBase{
	public const string XmlName = "ActionGroup";
	//[XmlArray("ActionList")]
	//[XmlArrayItem("Action")]
	//[SerializeField]
	private List<ActionBase> actionList_;//{ get{ return actionList_; } }
	//[SerializeField]
	/*public ActionGroup(POIScript parentPOI, float multiplier = 1f) add setter/adder for actionList_ before uncommenting
		:base(parentPOI){
		actionList_ = new List<ActionBase>();
	}*/
	public ActionGroup(){
		actionList_ = new List<ActionBase> ();
	}	
	public ActionGroup(float multiplier){
		actionList_ = new List<ActionBase> ();
	}
	public ActionGroup(List<ActionBase> actionList, float multiplier = 1f){
		actionList_ = new List<ActionBase> (actionList);
	}
	public ActionGroup(XmlNode node){
		if (node.Name != XmlName) {
			throw(new Exception ("bad ActionGroup Xml"));
			return;
		}
		actionList_ = new List<ActionBase> ();
		foreach (XmlNode subNode in node.ChildNodes) {
			ActionBase temp;
			if (ActionBase.extractAction (subNode, out temp))
				actionList_.Add (temp);
		}
	}
	
	public override bool dryRun(float multiplier){ 
		bool total = true;
		foreach ( ActionBase action in actionList_){
			total |= action.dryRun(multiplier);
		}
		return total;
	}
	
	public override void execute(float multiplier){
		foreach ( ActionBase action in actionList_){
			action.execute(multiplier);
		}
	}

	public override void linkPOI(POI_XML parentPOI){
		base.linkPOI(parentPOI_);
		foreach ( ActionBase action in actionList_){
			action.linkPOI(parentPOI);
		}
	}
	
	public override XmlNode saveXML(XmlDocument xmlDoc){
		XmlNode node = xmlDoc.CreateElement(XmlName);

		foreach ( ActionBase action in actionList_){
			node.AppendChild( action.saveXML(xmlDoc));
		}
		
		return node;
	}
	
	public override bool mergeAction(ActionBase newAction){ 
		if (newAction.GetType () == this.GetType () ) {
			ActionGroup actionGroup = (ActionGroup)newAction;
			foreach (ActionBase subAction in actionGroup.actionList_) {
				mergeAction (subAction);
			}
		} else {
			bool merged = false;
			foreach ( ActionBase action in actionList_){
				if (action.mergeAction(newAction) == true){
					merged = true;
					break;
				}
			}
			if (!merged){
				newAction.linkPOI (parentPOI_);
				actionList_.Add (newAction);
			}
		}
		return true;
	}
	
	/*public override void scale (float multiplicator){
		foreach (ActionBase action in actionList_)
			action.scale (multiplicator);
	}*/
}


public class ActionPerWorker : ActionBase{
	//[SerializeField]
	public const string XmlName = "ActionPerWorker";
	private ActionBase action_;//{ get{ return action_; } }
	/*public ActionPerWorker(POIScript parentPOI) add setter for action_ before uncommenting
	:base(parentPOI){
		action_ = new ActionBase();
	}*/
	public ActionPerWorker(ActionBase action){
		action_ = action;
	}
	public ActionPerWorker(XmlNode node){
		if (node.Name != XmlName){
			throw(new Exception ("bad ActionPerWorker Xml"));
			return;
		}
		if (!ActionBase.extractAction (node, out action_))
			throw(new Exception ("bad ActionPerWorker Xml"));

	}
	//parentPOI_.workerList_.Count
	public override bool dryRun(float multiplier){ 
		if (action_ != null)
			return action_.dryRun (multiplier * parentPOI_.workerList_.Count);
		else
			return false;
	}
	
	public override void execute(float multiplier){
		if (action_ != null)
			action_.execute(multiplier * parentPOI_.workerList_.Count);
	}
	
	public override void linkPOI(POI_XML parentPOI){
		base.linkPOI(parentPOI_);
		if (action_ != null)
			action_.linkPOI(parentPOI);
	}

	public override XmlNode saveXML(XmlDocument xmlDoc){
		XmlNode node = xmlDoc.CreateElement("ActionPerWorker");

		node.AppendChild( action_.saveXML(xmlDoc));
		
		return node;
	}
	
	public override bool mergeAction(ActionBase newAction){
		if (newAction.GetType () == this.GetType ()) {
			ActionPerWorker actionPW = (ActionPerWorker)newAction;
			return (action_.mergeAction(actionPW.action_));
		}
		return false;
	}
	
	/*public override void scale (float multiplicator){
		action_.scale (multiplicator);
	}*/
}
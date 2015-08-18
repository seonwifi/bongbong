using UnityEngine;
using System.Collections;

public class UnitFactory 
{

	// Use this for initialization
	static public Unit CreateUnit (string unitResourceName)
	{
		//unit
		GameObject goPrefab = Resources.Load<GameObject> ("Unit/" + unitResourceName);
		if(goPrefab == null)
		{
			Debug.LogError("Unit Create Error => " + unitResourceName);
			return null;
		}
		GameObject l_gameObject = GameObject.Instantiate<GameObject> (goPrefab);
		l_gameObject.SetActive(true);
		Unit unit = l_gameObject.GetComponent<Unit>();
		  
		//searcher
		UnitSearch unitSearcher = UnitSearch.CreateUnitSearch (unit.MyTransform, 15);

		//gage bar
		unit.m_unitGagebar = GameMgr.Ins.m_unitLocation.m_gagebar.GetComponent<UxUnitGagebar>();
 
		//ai
		UnitAi unitAi = new UnitAi ();
		unitAi.Init(unit, unitSearcher);
		return unit;
	}
 
}

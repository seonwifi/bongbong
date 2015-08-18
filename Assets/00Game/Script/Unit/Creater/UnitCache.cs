using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitCache  
{
	public string m_unitName;
	public int    m_cacheId = 0;
	public Stack<Unit>  m_SleepUnitList = new Stack<Unit>();
	// Use this for initialization
	public UnitCache (string unitResourceName) 
	{
		m_unitName = unitResourceName;
	}

	public void Add (Unit sleepUnit) 
	{
		m_SleepUnitList.Push (sleepUnit);
	}

	// Update is called once per frame
	public Unit GetUnit () 
	{
		if(m_SleepUnitList.Count == 0)
		{
			return UnitFactory.CreateUnit(m_unitName);
		}
		return m_SleepUnitList.Pop ();
	}
}

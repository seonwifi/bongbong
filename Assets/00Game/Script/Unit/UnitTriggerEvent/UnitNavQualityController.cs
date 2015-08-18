using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitNavQualityController : MonoBehaviour 
{
	static public UnitNavQualityController m_Current = null;
	Unit m_unit = null;
	//LinkedList<GameObject> m_enterList = new LinkedList<GameObject>();

	void Awake()
	{
		m_unit = this.gameObject.GetComponentInParent<Unit>();
	}

	public int m_enterCount = 0;
	void OnTriggerEnter(Collider other)
	{ 
		if(GameLayer.Unit == other.gameObject.layer)
		{ 
			m_enterCount++;
			if(m_enterCount > 1)
			{
				m_Current = this;
				if(m_unit) m_unit.SetNavQuality(ObstacleAvoidanceType.LowQualityObstacleAvoidance); 
				m_Current = null;
			} 
			else
			{
				m_Current = this;
				if(m_unit) m_unit.SetNavQuality(ObstacleAvoidanceType.NoObstacleAvoidance); 
				m_Current = null;
			}
		}
		//other.gameObject.layer;

	}
//	void OnTriggerStay(Collider other)
//	{
//
//	}

 
	void OnTriggerExit(Collider other)
	{ 
		if(GameLayer.Unit == other.gameObject.layer)
		{
			m_enterCount--;
			if (m_enterCount <= 1) 
			{
				if(m_unit)
				{
					m_Current = this;
					m_unit.SetNavQuality(ObstacleAvoidanceType.NoObstacleAvoidance);
					m_Current = null;
				}
			} 
		} 
	}
}

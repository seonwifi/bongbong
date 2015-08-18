using UnityEngine;
using System.Collections;

public class UnitLife
{
	public System.Action<UnitLife> m_OnValueChanged = null;
	
	float m_current 	= 100; 
	int m_min			= 0; 
	int m_max			= 100; 
	
	public int MinHP
	{
		get
		{ 
			return m_min;
		}
		
		set
		{
			m_min = value;
		}
	}
	public int MaxHP
	{
		get
		{
			return m_max;
		}
		
		set
		{
			m_max = value;
		}
	}
	
	
	public float HP
	{
		get
		{
			return m_current;
		}
		set
		{  
			m_current = value;
			if(m_current > m_max) m_current = m_max;
			if(m_current < m_min) m_current = m_min; 

			if(m_OnValueChanged != null)
			{ 
				m_OnValueChanged(this);
			}
		}
	}
}

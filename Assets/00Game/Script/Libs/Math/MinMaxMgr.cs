using UnityEngine;
using System.Collections;

//namespace System
//{
//	public delegate void Action<T1> (T1 arg1);
//}

public class IntMinMax 
{
	 
	public System.Action<IntMinMax> m_OnValueChanged = null;

	int m_current 	= int.MinValue; 
	int m_min		= int.MinValue; 
	int m_max		= int.MinValue; 

	public int Min
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
	public int Max
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


	public int Value
	{
		get
		{
			return m_current;
		}
		set
		{ 
			if(value > Max) value = Max;
			if(value < Min) value = Min;

			if(m_OnValueChanged != null && m_current != value)
			{
				m_current = value;
				m_OnValueChanged(this);
			}
		}
	}
	
}

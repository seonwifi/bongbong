using UnityEngine;
using System.Collections;

public abstract class Mount : MonoBehaviour, IUpdate
{
	public virtual void EnableUpdate (){}
	public abstract void UpdateFrame ();  
	public virtual void DisableUpdate (){}	  

	bool m_isEnable = true;
	public virtual bool enableUpdate 
	{
		get
		{
			return m_isEnable;
		}
		set
		{
			m_isEnable = value;
			if(m_isEnable)
			{
				EnableUpdate (); 
			}
			else
			{
				DisableUpdate (); 
			}
		}
	}

	public abstract void StartFire (eUnitType findType);
	public abstract	void EndFire () ;

	public abstract bool IsFire{get;}

}

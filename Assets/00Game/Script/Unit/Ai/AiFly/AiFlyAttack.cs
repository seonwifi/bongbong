using UnityEngine;
using System.Collections;

public class AiFlyAttack : IAiProcess 
{
	bool m_init = false;
	FlyUnitAgent m_FlyUnitAgent = null;
	 //m_mountList
	public bool IsEnd ()
	{  
		if(m_ai.m_TargetUnit == null)
		{
			return true; 
		}
		else if(m_ai.m_TargetUnit.m_ai.m_dead)
		{
			return true;
		}
		else if(!m_ai.UnitTargetIn())
		{
			return true;
		}
		return false;
	}

	public override void BeginState (IAiProcess currentState)
	{
		if(m_init == false)
		{
			m_init = true;
			for(int i = 0; i < m_ownerUnit.m_mountList.Count; ++i)
			{
				m_ai.m_UpdateMgr.Add(m_ownerUnit.m_mountList[i]);
			}

		} 
		if(m_FlyUnitAgent == null)
		{
			m_FlyUnitAgent = m_ai.m_UpdateMgr.GetUpdateClass<FlyUnitAgent> ();
		}
 
	}
	
	public override void EndState (IAiProcess nextState)
	{
		for(int i = 0; i < m_ownerUnit.m_mountList.Count; ++i)
		{
			m_ownerUnit.m_mountList[i].EndFire();
		}
	}
 
	public override void Update ()
	{  
		if (IsEnd ()) 
		{
			m_ai.m_TargetUnit = null;
			NextProcess(eAiProcess.fly_run);
		} 
		else
		{
			if(m_ai.m_TargetUnit != null && m_FlyUnitAgent != null)
			{ 
				m_FlyUnitAgent.TargetFoward = m_ai.m_TargetUnit.Position - m_ownerUnit.Position;
				if(m_FlyUnitAgent.IsCurrentFowardTargetFoward)
				{
					for(int i = 0; i < m_ownerUnit.m_mountList.Count; ++i)
					{
						if(!m_ownerUnit.m_mountList[i].IsFire)
						{
							m_ownerUnit.m_mountList[i].StartFire(m_ai.EnermyType);
						} 
					}
				}
			}
		} 
	}
}
















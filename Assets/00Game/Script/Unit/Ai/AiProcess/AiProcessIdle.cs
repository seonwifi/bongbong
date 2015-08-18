using UnityEngine;
using System.Collections;

public class AiProcessIdle : IAiProcess
{
	public AiProcessIdle()
	{
	 
	} 
	float m_startTime = 0;
	public override void BeginState (IAiProcess currentState)
	{
//		if(currentState != null)
//		{
//
//		}
		m_startTime = Time.realtimeSinceStartup;
 
	}

	public override void EndState (IAiProcess nextState)
	{
//		if(nextState != null)
//		{
//
//		}
	}

	public override void Update ()
	{
		m_ownerUnit.m_ai.FindEnermy();

		if(m_ownerUnit.m_ai.m_TargetUnit != null)
		{
			if(m_ownerUnit.m_ai.UnitTargetIn())
			{
				m_ownerUnit.m_ai.SetNextProcess(eAiProcess.attack);
			}
			else
			{
				m_ownerUnit.m_ai.SetNextProcess(eAiProcess.run);
			}
		}
	}


}

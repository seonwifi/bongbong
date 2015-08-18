using UnityEngine;
using System.Collections;

public class AiProcessDie : IAiProcess
{
	float m_time = 0;
	bool m_dead = false;
	public UnityEngine.Events.UnityAction m_UnityActionDeadStart = null;
	public UnityEngine.Events.UnityAction m_UnityActionDeadEnd = null;

	public override void BeginState (IAiProcess currentState)
	{ 
		m_ownerUnit.m_ai.m_dead = true;
		m_ownerUnit.StopMove();
		m_ownerUnit.Die();
		m_ownerUnit.EnableNavAgent = false; 
		m_ownerUnit.m_unitGagebar.gameObject.SetActive(false);
 
		m_time = 2;

		if(m_UnityActionDeadStart != null &&
		   m_UnityActionDeadStart.Target != null)
		{
			m_UnityActionDeadStart.Invoke(); 
		}
	}

	public override void EndState (IAiProcess nextState)
	{
		if(m_UnityActionDeadEnd != null &&
		   m_UnityActionDeadEnd.Target != null)
		{
			m_UnityActionDeadEnd.Invoke(); 
		}
		GameMgr.Ins.m_unitMgr.RemoveUnit(m_ownerUnit);
		m_dead = true;
	}

	public override void Update ()
	{
		if (m_dead == false) 
		{
			m_time -= Time.deltaTime;
			if(m_time < 0)
			{
				NextProcess(eAiProcess.Max);
			}
		} 
	} 
}

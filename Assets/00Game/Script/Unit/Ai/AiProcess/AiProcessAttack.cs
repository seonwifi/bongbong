using UnityEngine;
using System.Collections;

public class AiProcessAttack  : IAiProcess
{

	public override void BeginState (IAiProcess currentState)
	{
 
		//m_currentSkill = this.OwnerUnit.m_baseSkill;

		if(m_ownerUnit.IsMove())
		{
			m_ownerUnit.StopMove();
		}
		if(!m_ownerUnit.IsAttacking())
		{
			m_ownerUnit.Attack(0);
		} 

		if(m_ai.m_TargetUnit != null)
		{
			m_ownerUnit.LookTarget(m_ai.m_TargetUnit.Position);
		} 
		m_ownerUnit.SetNavQuality(ObstacleAvoidanceType.NoObstacleAvoidance);
	}

	public override void EndState (IAiProcess nextState)
	{
		 
	}

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

	public override void Update ()
	{  
		if (IsEnd ()) 
		{
			m_ai.m_TargetUnit = null;
			NextProcess(eAiProcess.run);
		} 
		else
		{
			if(!m_ownerUnit.IsAttacking())
			{
				m_ownerUnit.Attack(0); 
			}  
		} 
	}

	public bool AttackEnable ()
	{
		return true;
	}

 
}

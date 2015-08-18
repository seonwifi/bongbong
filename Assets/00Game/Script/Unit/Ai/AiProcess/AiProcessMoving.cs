using UnityEngine;
using System.Collections;

public class AiProcessMoving : IAiProcess {

 
	public AiProcessMoving()
	{
 
	} 

	bool m_moving = true;

 
	Vector3	m_lastMoveTarget = Vector3.zero;
	public override void BeginState ( IAiProcess currentState)
	{
		if(m_ai.m_TargetUnit != null)
		{
			m_lastMoveTarget = m_ai.m_endOfTarget;
			m_ownerUnit.Move(m_ai.m_TargetUnit.Position);  
		}
		else
		{
			m_lastMoveTarget = m_ai.m_endOfTarget;
			m_ownerUnit.Move(m_ai.m_endOfTarget); 
		}
	}
	
	public override void EndState ( IAiProcess nextState)
	{

	}
	
	public override void Update ()
	{
		if(m_ownerUnit == null)
		{
			return;
		}
		if(m_ownerUnit.EnableMoving)
		{
			if(m_ai.m_TargetUnit != null)
			{
				
				if(m_ai.UnitTargetIn())
				{
					m_ai.SetNextProcess(eAiProcess.attack);  
				}
				else
				{
					if(m_lastMoveTarget != m_ai.m_TargetUnit.Position)
					{ 
						m_lastMoveTarget = m_ai.m_TargetUnit.Position;
						m_ownerUnit.Move(m_ai.m_TargetUnit.Position);  
					} 
					m_ai.FindEnermy();
				}
				if(m_ai.m_dead)
				{
					m_ownerUnit.StopMove(); 
					m_lastMoveTarget = m_ai.m_TargetUnit.Position;
					m_ai.m_TargetUnit = null;
				}
			}
			else
			{
				m_ownerUnit.SetNavQuality(ObstacleAvoidanceType.LowQualityObstacleAvoidance);
				if(m_lastMoveTarget != m_ai.m_endOfTarget)
				{
					m_lastMoveTarget = m_ai.m_endOfTarget;
					
					m_ownerUnit.Move(m_ai.m_endOfTarget); 
				} 
				else  
				{
					if(m_ownerUnit.IsMovingEndOfTarget())
					{
						m_ai.SetNextProcess(eAiProcess.idle); 
					} 
				} 
				m_ai.FindEnermy();
			}
		}
		else
		{
			if(m_ai.m_TargetUnit != null)
			{
				m_ai.SetNextProcess(eAiProcess.attack); 
			}
			else
			{
				m_ai.FindEnermy();
			}

		}  
	}
 
	

}

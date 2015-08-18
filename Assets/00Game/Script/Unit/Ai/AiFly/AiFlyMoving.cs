using UnityEngine;
using System.Collections;

public class AiFlyMoving : IAiProcess 
{

	bool m_moving = true;
	FlyUnitAgent m_flyUnitAgent = null;
	Vector3	m_lastMoveTarget = Vector3.zero;
	public override void BeginState ( IAiProcess currentState)
	{
		if(m_ownerUnit.EnableMoving)
		{
			if(m_flyUnitAgent == null)
			{
				m_flyUnitAgent 				= new FlyUnitAgent(); 
				m_flyUnitAgent.m_unit		= m_ownerUnit;

				m_ai.m_UpdateMgr.Add(m_flyUnitAgent);
			}

			m_flyUnitAgent.enableUpdate = true;

			if(m_ai.m_TargetUnit != null)
			{ 
				SetTarget (m_ai.m_TargetUnit.Position);
			}
			else
			{
				SetTarget (m_ai.m_endOfTarget); 
				
			}
		} 
	}

	public   void SetTarget (Vector3 newTarget)
	{
		m_lastMoveTarget 		= newTarget;  
		Vector3 movingTarget 	= m_ownerUnit.Position;
		movingTarget.z 			= m_lastMoveTarget.z;

		if(m_flyUnitAgent != null) m_flyUnitAgent.TargetMove = movingTarget;
	}

	public  bool IsEndOfTarget ()
	{
		float zVector1 = m_ai.m_startPosition.z - m_ai.m_endOfTarget.z;
		float zVector2 = m_ownerUnit.Position.z - m_ai.m_endOfTarget.z;
		if(zVector1*zVector2 <= 0)
		{
			return true;
		}
		return false;
	}

	public override void EndState ( IAiProcess nextState)
	{
		if(m_flyUnitAgent != null) m_flyUnitAgent.StopMove(); 
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
					m_ai.SetNextProcess(eAiProcess.fly_attack);  
				}
				else
				{
					if(m_lastMoveTarget != m_ai.m_TargetUnit.Position)
					{ 
						SetTarget (m_ai.m_TargetUnit.Position); 
					} 
					m_ai.FindEnermy();
				}

				if(m_ai.m_dead)
				{
					if(m_flyUnitAgent != null) m_flyUnitAgent.StopMove(); 

					m_lastMoveTarget 	= m_ai.m_TargetUnit.Position;
					m_ai.m_TargetUnit 	= null;
				}
			}
			else
			{ 
				if(m_lastMoveTarget != m_ai.m_endOfTarget)
				{
					SetTarget (m_ai.m_endOfTarget);  
				} 
				else  
				{
					if(IsEndOfTarget())
					{
						m_ai.SetNextProcess(eAiProcess.fly_idle); 
					} 
				} 

				m_ai.FindEnermy();
			}
		}
		else
		{
			if(m_ai.m_TargetUnit != null)
			{
				m_ai.SetNextProcess(eAiProcess.fly_attack); 
			}
			else
			{
				m_ai.FindEnermy();
			}
			
		}  
	}

}

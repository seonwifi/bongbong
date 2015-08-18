using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eAiProcess
{
	idle = 0,
	run,
	attack,
	die, 
	come_into_the_world,
	fly_run,
	fly_attack,
	fly_idle,
	fly_die,
	Max,
}

public class UnitAi 
{
 
#region InitData
	public Unit 			m_unit;
	public UnitSearch 		m_enermySearcher;
	public float			m_searchRange = 10;
	public eUnitType		m_TeamType; 
	public UnitAttribute	m_UnitAttribute;
	public eUnitType 		EnermyType
	{
		get
		{
			return m_TeamType == eUnitType.Armmy ? eUnitType.Enermy : eUnitType.Armmy;
		}
	}
#endregion

#region AiData
	public Vector3 		m_lastMoveTarget = Vector3.zero;
	public Vector3 		m_endOfTarget;
	public Vector3 		m_startPosition;
	public Unit 		m_TargetUnit;  
	public bool 		m_dead = false;
	bool 				m_isIdle = false;
#endregion

#region AiProcess

	List<IAiProcess> m_AiProcessList 	= new List<IAiProcess>();
	IAiProcess m_currentAiProcess 		= null;
	IAiProcess m_nextAiProcess 			= null;
	public UpdateMgr  m_UpdateMgr 				= new UpdateMgr();
#endregion

	//목적지 앞쪽..
	public float FrontZ
	{
		get
		{
			if(m_TeamType == eUnitType.Armmy)
			{
				return m_unit.Position.z + m_unit.UnitSize;
			}

			return m_unit.Position.z - m_unit.UnitSize;
		}
	}

	public void AddState(eAiProcess eaiProcessName, IAiProcess aiProcess)
	{
		 
		if(aiProcess != null)
		{
			m_AiProcessList[(int)eaiProcessName] = aiProcess;
			aiProcess.SetOwnerUnit(this.m_unit, eaiProcessName);

		}
		else
		{
			Debug.LogError("Error State is null");
		}  
	}
	public void SetNextProcess(eAiProcess eaiProcessName)
	{
		if(eaiProcessName == eAiProcess.Max)
		{
			m_nextAiProcess = null;
		}
		else
		{
			m_nextAiProcess = m_AiProcessList [(int)eaiProcessName];
		}

	}


	public void Init(Unit myUnit, UnitSearch searcher)
	{
		m_unit 				= myUnit;
		m_unit.m_ai 		= this; 

		m_enermySearcher 	= searcher;
		if(m_enermySearcher)m_enermySearcher.m_myAi		= this;

		m_AiProcessList.Capacity = (int)eAiProcess.Max;
 
		for(int i = 0; i < (int)eAiProcess.Max; ++i)
		{
			m_AiProcessList.Add(null);
		}
 
		AddState( eAiProcess.come_into_the_world, new AiProcessComeIntoTheWorld(eAiProcess.fly_run)); 
		AddState( eAiProcess.idle, new AiProcessIdle());
		AddState( eAiProcess.run, new AiProcessMoving());
		AddState( eAiProcess.attack, new AiProcessAttack());
		AddState( eAiProcess.die, new AiProcessDie());  
		AddState( eAiProcess.fly_run, new AiFlyMoving()); 
		AddState( eAiProcess.fly_attack, new AiFlyAttack()); 
		AddState( eAiProcess.fly_idle, new AiFlyIdle()); 
		AddState( eAiProcess.fly_die, new AiFlyDie()); 

		SetNextProcess (eAiProcess.come_into_the_world);
		m_UnitAttribute = new UnitAttribute ();
	}

	public void SetEndTarget(Vector3 target)
	{
		m_endOfTarget = target;
	}
	public void SetStartPos(Vector3 startPos)
	{
		m_startPosition = startPos;
	}
	public void FindEnermy()
	{
		if (m_dead)
		{
			return; 
		}
 
		
		Unit l_Unit = GameMgr.Ins.m_unitMgr.FindCombatUnit(m_unit.Position, m_unit.UnitSize, m_searchRange, EnermyType, null);

		if(l_Unit != null)
		{
			m_TargetUnit = l_Unit; 
		}   
	}

	public bool UnitTargetIn()
	{ 
		if(m_TargetUnit == null)
		{
			return false;
		}
		
		float zDist = m_TargetUnit.Position.z - m_unit.Position.z;
		if (zDist < 0)
			zDist *= 1;

		float l_bothSize = m_TargetUnit.UnitSize + m_unit.UnitSize;
		l_bothSize += m_unit.AttackRange;
		if(zDist <= l_bothSize*l_bothSize)
		{
			return true;
		}
		
		return false;
	}

//	public bool UnitTargetIn()
//	{ 
//		if(m_TargetUnit == null)
//		{
//			return false;
//		}
//
//		Vector3 v_PosDist = m_TargetUnit.Position - m_unit.Position;
// 
//		float l_bothSize = m_TargetUnit.UnitSize + m_unit.UnitSize;
//		l_bothSize += m_unit.AttackRange;
//		if(v_PosDist.sqrMagnitude <= l_bothSize*l_bothSize)
//		{
//			return true;
//		}
//  
//		return false;
//	}


	public void UpdateAI()
	{ 
		if(m_nextAiProcess != m_currentAiProcess)
		{
			IAiProcess nextAiProcess = m_nextAiProcess;
			IAiProcess currentProcess = m_currentAiProcess;
			if(currentProcess != null) currentProcess.EndState(nextAiProcess); 
			if(nextAiProcess != null)  nextAiProcess.BeginState(currentProcess);

			m_currentAiProcess = m_nextAiProcess; 
		}

		if(m_currentAiProcess != null)
		{
			m_currentAiProcess.Update();
		}

		if(m_UnitAttribute.HP <= 0 && m_dead == false)
		{
			m_dead = true;
			SetNextProcess(eAiProcess.fly_die); 
		}

		m_UpdateMgr.Update ();
		 
	}

	public void SetDamage(float damage)
	{
		m_UnitAttribute.HP -= damage;
	}
}

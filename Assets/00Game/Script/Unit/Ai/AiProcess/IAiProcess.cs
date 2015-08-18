using UnityEngine;
using System.Collections;

public abstract class IAiProcess
{  
	protected UnitAi m_ai;
	protected Unit m_ownerUnit = null;
	public Unit OwnerUnit
	{
		get
		{
			return m_ownerUnit;
		}
	}

	public  virtual void SetOwnerUnit(Unit ownerUnit, eAiProcess	state)
	{
		m_ownerUnit = ownerUnit;
		m_ai = m_ownerUnit.m_ai;
		m_State     = state;
	}

	protected eAiProcess	m_State = eAiProcess.Max; 

	public eAiProcess	State
	{
		get
		{
			return m_State;
		}
	}
	public abstract void BeginState (IAiProcess currentState);  
	public abstract void EndState (IAiProcess nextState); 
	public abstract void Update (); 


	virtual protected void NextProcess(eAiProcess nextAiProcess)
	{
		m_ai.SetNextProcess (nextAiProcess);
	}
 
//	public override void BeginState (IAiProcess currentState)
//	{
////		if(currentState != null)
////		{
////			 
////		}  
////		else
////		{
////
////		}
//	}
//	
//	public override void EndState (IAiProcess nextState)
//	{
////		if(nextState != null)
////		{
////			
////		}  
////		else
////		{
////			
////		} 
//	}
//	
//	public override void Update ()
//	{
//		
//	} 

}

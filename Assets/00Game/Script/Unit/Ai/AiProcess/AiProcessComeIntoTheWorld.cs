using UnityEngine;
using System.Collections;

public class AiProcessComeIntoTheWorld : IAiProcess {

	float m_waitTime = 0;
	float m_waitTimeMax = 0.3f;
	eAiProcess m_nextProcess;

	public AiProcessComeIntoTheWorld(eAiProcess nextProcess)
	{
		m_nextProcess = nextProcess;
	}

	public override void BeginState (IAiProcess currentState)
	{
		if(currentState != null)
		{
			 
		}  
		else
		{

		}
		m_waitTime = m_waitTimeMax;
	}
	
	public override void EndState (IAiProcess nextState)
	{
		if(nextState != null)
		{
			
		}  
		else
		{
			
		} 
	}
	
	public override void Update ()
	{
		m_waitTime -= Time.deltaTime;
		if(m_waitTime < 0)
		{
			m_ownerUnit.m_ai.SetNextProcess(m_nextProcess);
		}
	}//*/
}

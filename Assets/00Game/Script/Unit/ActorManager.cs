using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorManager : MonoBehaviour {

	public GameObject []m_enermyPoss;
	public GameObject []m_armmyPoss;
	public GameObject m_Actor;
	public GameObject m_ActorEnermy;
	public int count = 5;
	List<Unit>  m_ArmmyList = new List<Unit>();
	List<Unit>  m_EnermyList = new List<Unit>();

	// Use this for initialization
	void Start () 
	{

//		//count = m_enermyPoss.Length;
//		for(int i = 0; i < count; ++i)
//		{
//			GameObject l_gameObject = GameObject.Instantiate(m_Actor) as GameObject;
//			l_gameObject.SetActive(true);
//			l_gameObject.transform.position = m_armmyPoss[i].transform.position;
//			Unit l_NavTest = l_gameObject.GetComponent<Unit>();
//			m_ArmmyList.Add(l_NavTest);
//		}
//
//		for(int i = 0; i < count; ++i)
//		{
//			GameObject l_gameObject = GameObject.Instantiate(m_ActorEnermy) as GameObject;
//			l_gameObject.SetActive(true);
//			l_gameObject.transform.position = m_enermyPoss[i].transform.position;
//			Unit l_NavTest = l_gameObject.GetComponent<Unit>();
//			m_EnermyList.Add(l_NavTest);
//		}
//
//		for(int  i = 0; i < count; ++i)
//		{
//			m_ArmmyList[i].m_ActorManager = this;
//			m_ArmmyList[i].m_EnermyId = i;
//			m_ArmmyList[i].SetTeamType(Unit.TeamType.Armmy);
//			m_ArmmyList[i].SetMoving(m_EnermyList[i]); 
//		}
//
//		for(int  i = 0; i < count; ++i)
//		{
//			m_EnermyList[i].m_ActorManager = this;
//			m_EnermyList[i].m_EnermyId = (count-1) -i;
//			m_EnermyList[i].SetTeamType(Unit.TeamType.Enermy);
//			m_EnermyList[i].SetMoving(m_ArmmyList[i]); 
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Unit GetEnermy(int index)
	{
		return m_EnermyList[index];
	}
	public Unit GetArmmy(int index)
	{
		return m_ArmmyList[index];
	}
}

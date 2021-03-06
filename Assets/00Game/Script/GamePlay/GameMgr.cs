using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProduceEnergeMgr
{
	float m_produceEnerge 		= 30;
	float m_produceEnergeMax 	= 100;
	float m_produceEnergeSpeed 	= 5;

	public float ProduceEnergePer
	{
		get
		{
			return m_produceEnerge/m_produceEnergeMax;
		}
		set
		{
			m_produceEnerge = m_produceEnergeMax;
		}
	}

	public int ProduceEnergeInt
	{
		get
		{
			return (int)m_produceEnerge;
		}
	}
	public int ProduceEnergeMaxInt
	{
		get
		{
			return (int)m_produceEnergeMax;
		}
	}
	public float ProduceEnerge
	{
		get
		{
			return m_produceEnerge;
		}
		set
		{
			m_produceEnerge = value;
			m_produceEnerge = m_produceEnerge > m_produceEnergeMax ? m_produceEnergeMax :  m_produceEnerge < 0 ? 0 : m_produceEnerge;
		}
	}

	public float ProduceEnergeMax
	{
		get
		{
			return m_produceEnergeMax;
		}
	}

	public bool Use(float useValue)
	{
		if(m_produceEnerge - useValue < 0)
		{
			return false;
		}
		m_produceEnerge -= useValue;
		m_useWaitTime = m_useWaitTimeMax;
		return true;
	}

	public void Init()
	{
		m_produceEnerge 		= 30;
		m_produceEnergeMax 		= 100;
		m_produceEnergeSpeed 	= 5;
	}
	float m_useWaitTime = 0;
	float m_useWaitTimeMax = 0.25f;
	public void Update()
	{   
		m_useWaitTime -= Time.deltaTime;
		if (m_useWaitTime > 0)
			return;
 
		if(m_produceEnerge < m_produceEnergeMax)
		{
			ProduceEnerge = m_produceEnerge + Time.deltaTime*m_produceEnergeSpeed;
		}
	}
}

public class GameMgr : MonoBehaviour 
{
	static GameMgr m_Ins;
	static public GameMgr Ins
	{
		get 
		{
			if(m_Ins == null)
			{
				(new GameObject("GameMgr")).AddComponent<GameMgr>();
			}
			return m_Ins;
		}
	}

	public GameMapSetting 	m_unitLocation;
	public UnitMgr		 	m_unitMgr 		= new UnitMgr();
	 
	List<UnitCache> 		m_unitNameList 	= new List<UnitCache>();

	public ProduceEnergeMgr m_produceEnerge = new ProduceEnergeMgr();	
 

	void Awake() 
	{
		m_Ins = this;

		UnitCache unitCache = null;

		unitCache 			= new UnitCache ("U_00");
		m_unitNameList.Add (unitCache);
		unitCache.m_cacheId = m_unitNameList.Count - 1;

		unitCache 			= new UnitCache ("U_00");
		m_unitNameList.Add (unitCache);
		unitCache.m_cacheId = m_unitNameList.Count - 1;
		 
		unitCache 			= new UnitCache ("commandCenter");
		m_unitNameList.Add (unitCache);
		unitCache.m_cacheId = m_unitNameList.Count - 1;

		m_produceEnerge.Init ();
	}

	public void StartGame(GameMapSetting unitLocation)
	{
		m_unitLocation = unitLocation; 

		Unit unit 						= 	GameMgr.Ins.CreateUnit (2);
		unit.Position 					= GameMgr.Ins.m_unitLocation.m_EnermyLocation.CommandCenter;
		unit.m_ai.m_UnitAttribute.HP 	= 	unit.m_ai.m_UnitAttribute.HPIntMax;
		unit.EnableMoving 				= false;
		GameMgr.Ins.m_unitMgr.AddEnermy (unit);

		unit 							= GameMgr.Ins.CreateUnit (2);
		unit.Position 					= GameMgr.Ins.m_unitLocation.m_ArrmyLocation.CommandCenter;
		unit.m_ai.m_UnitAttribute.HP 	= unit.m_ai.m_UnitAttribute.HPIntMax;
		unit.EnableMoving 				= false;
		GameMgr.Ins.m_unitMgr.AddArmmy (unit);
	}
 
	
	// Update is called once per frame
	void Update () 
	{
		m_produceEnerge.Update ();
		m_unitMgr.UpdateMgr ();


	}

	void LateUpdate () 
	{
		if(GameCamera.Ins)
		{
			GameCamera.Ins.UpdatePos();
		}
		
		m_unitMgr.UpdateHp();
	}

	public Unit CreateUnit(int v_id)
	{
		return m_unitNameList[v_id].GetUnit();
	}

	public void OnDeadBeginUnit(Unit deadUnit)
	{

	}
	public void OnDeadEndUnit(Unit deadUnit)
	{
		if(m_armmyCommandCenter == deadUnit)
		{
			if(!m_isGameOver)
			{
				m_isGameOver = true;
				m_gameUx.Lose(); 
			}

		}

		else if(m_enermyCommandCenter == deadUnit)
		{
			if(!m_isGameOver)
			{
				m_isGameOver = true;
				m_gameUx.Victory(); 
			} 
		}
	}

	public Vector3 GetMapToNomalPos(Vector3 worldMapPos)
	{
		float localPosX = worldMapPos.x - m_unitLocation.m_mapLeft.position.x;
		float localPosZ = worldMapPos.z - m_unitLocation.m_ArrmyLocation.CommandCenter.z;

		float mapWidth  = m_unitLocation.m_mapRight.position.x - m_unitLocation.m_mapLeft.position.x;
		float mapHeight = m_unitLocation.m_EnermyLocation.CommandCenter.z - m_unitLocation.m_ArrmyLocation.CommandCenter.z;

		Vector3 nomalPos = new Vector3(localPosX/mapWidth, localPosZ/mapHeight, 0);
		return nomalPos;
	}

	bool m_isGameOver = false;
	Unit m_armmyCommandCenter = null;
	Unit m_enermyCommandCenter = null;

	UxGame m_gameUx = null;
}

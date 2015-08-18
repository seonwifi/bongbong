using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization;
 

public class UxGame : MonoBehaviour 
{ 

	public UnityEngine.UI.Slider m_produceEnergebar;
	public UnityEngine.UI.Text	 m_Text_ProduceEnergebar;
	public UnityEngine.UI.Image	 m_Image_minimapBG;
	public GameObject			 m_minimapUnitPrefab;
	System.Text.StringBuilder    m_StringBuilder_ProduceEnergebar = new System.Text.StringBuilder ();

	UxMinimapMgr m_minimapMgr = new UxMinimapMgr();

	void OnDestroy()
	{
		m_produceEnergebar 					= null;
		m_Text_ProduceEnergebar 			= null;
		m_Image_minimapBG 					= null;
		m_minimapUnitPrefab 				= null;
		m_StringBuilder_ProduceEnergebar 	= null;
		m_minimapMgr.Dispose ();
		m_minimapMgr = null;
		 
	}

	// Update is called once per frame
	public void OnClick (int UnitId)
	{
		UnityEngine.UI.Image Aaa;
		UnityEngine.UI.Button aa;

		if(GameMgr.Ins.m_produceEnerge.Use (10))
		{
			int skyPos = Random.Range(0, GameMgr.Ins. m_unitLocation.m_ArrmyLocation.SkyStartPosCount);

			Unit unit = GameMgr.Ins.CreateUnit (UnitId);
			//unit.Position = GameMgr.Ins.m_unitLocation.m_ArrmyLocation.GetSkyStartPos( GameMgr.Ins.m_unitLocation.m_ArrmyLocation.SkyStartPosCount-1);
			unit.Position = GameMgr.Ins.m_unitLocation.m_ArrmyLocation.GetSkyStartPos( skyPos);
			unit.m_ai.SetStartPos(unit.Position);
			unit.m_ai.SetEndTarget(GameMgr.Ins.m_unitLocation.m_ArrmyLocation.FinalDestination);
			unit.m_ai.m_UnitAttribute.HPIntMax = 200;
			unit.m_ai.m_UnitAttribute.HP = 200;
			GameMgr.Ins.m_unitMgr.AddArmmy (unit);
		} 
 
	}
 
	float m_produceEnerge = 0;
	public float ProduceEnerge
	{ 
		set
		{
			m_produceEnerge = value;
			m_produceEnergebar.value = m_produceEnerge;
		}
	}

 
	void Start()
	{
		 
		ProduceEnerge = GameMgr.Ins.m_produceEnerge.ProduceEnergePer;
		m_Text_ProduceEnergebar.text = GameMgr.Ins.m_produceEnerge.ProduceEnergeInt.ToString() + 
			"/" + GameMgr.Ins.m_produceEnerge.ProduceEnergeMaxInt.ToString();

		m_minimapUnitPrefab.SetActive (false);
		m_minimapMgr.Init (m_Image_minimapBG, m_minimapUnitPrefab);
	}

	float m_createTime = 0; 
 
	void LateUpdate()
	{
		System.WeakReference a;

		float produceEnergePer = GameMgr.Ins.m_produceEnerge.ProduceEnergePer;
		if(m_produceEnerge != produceEnergePer)
		{
			ProduceEnerge = produceEnergePer;
 
			m_StringBuilder_ProduceEnergebar.Remove(0, m_StringBuilder_ProduceEnergebar.Length);
			m_StringBuilder_ProduceEnergebar.Append(GameMgr.Ins.m_produceEnerge.ProduceEnergeInt);
			m_StringBuilder_ProduceEnergebar.Append('/');
			m_StringBuilder_ProduceEnergebar.Append(GameMgr.Ins.m_produceEnerge.ProduceEnergeMaxInt);
			m_Text_ProduceEnergebar.text = m_StringBuilder_ProduceEnergebar.ToString(); 
		} 
 

		m_createTime -= Time.deltaTime;

		if( m_createTime < 0 && isCreate == false)
		{ 
			//isCreate = true;
			m_createTime = Random.Range(1.0f, 5.0f);

			int create = Random.Range(1, 1);
			for(int i = 0; i < create; ++i)
			{
				Unit enermyUnit = GameMgr.Ins.CreateUnit(1); 
				int skyPos = Random.Range(0, GameMgr.Ins. m_unitLocation.m_EnermyLocation.SkyStartPosCount);

				//enermyUnit.Position = GameMgr.Ins. m_unitLocation.m_EnermyLocation.GetSkyStartPos(0);
				enermyUnit.Position = GameMgr.Ins. m_unitLocation.m_EnermyLocation.GetSkyStartPos(skyPos);
				enermyUnit.m_ai.SetEndTarget(GameMgr.Ins.m_unitLocation.m_EnermyLocation.FinalDestination);
				enermyUnit.m_ai.SetStartPos(enermyUnit.Position);
				GameMgr.Ins.m_unitMgr.AddEnermy(enermyUnit);
			} 
		} 
 
		m_minimapMgr.Update ();
	}
	bool isCreate = false;
	public void Victory()
	{

	}

	public void Lose()
	{
		
	}
}

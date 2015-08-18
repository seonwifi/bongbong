using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDataMgr : ITableStream
{
	public Dictionary<int, SkillData> m_DataDic = new Dictionary<int, SkillData>();
	
	public override void ParseCSV(CsvStream csvStream)
	{ 
		SkillData tableSample = new SkillData();
		tableSample.Load(csvStream);
		m_DataDic[tableSample.index] = tableSample;
	}
	public virtual void Add(SkillData tableSample)
	{
		m_DataDic[tableSample.index] = tableSample;
	}
	
	public virtual SkillData Get(int index)
	{
		SkillData outData = null;
		m_DataDic.TryGetValue(index, out outData);
		return outData;
	}
	public virtual void Clear()
	{
		m_DataDic.Clear ();
	}
}

public class SkillData
{
	public int		index = 1; 
	public int		damage = 10; 
	public float	attack_time = 1;
	public string 	motion_name = "attack";
	public float	attack_range = 10;
	ActionAttribute  m_UnitAttribute = ActionAttribute.Identity;


	public void Load(CsvStream csvStream)
	{
		csvStream.GetVar(ref index);
		csvStream.GetVar(ref damage);
		csvStream.GetVar(ref attack_time);
		csvStream.GetVar(ref motion_name); 
		csvStream.GetVar(ref attack_range); 

		csvStream.GetVar(ref  m_UnitAttribute.m_damage); 
		csvStream.GetVar(ref  m_UnitAttribute.m_damageAdd); 
		csvStream.GetVar(ref  m_UnitAttribute.m_damageMultiply); 
		csvStream.GetVar(ref  m_UnitAttribute.m_attackSpeed); 
		csvStream.GetVar(ref  m_UnitAttribute.m_attackSpeedAdd); 
		csvStream.GetVar(ref  m_UnitAttribute.m_attackSpeedMultiply); 
		csvStream.GetVar(ref  m_UnitAttribute.m_moveSpeed); 
		csvStream.GetVar(ref  m_UnitAttribute.m_moveSpeedAdd); 
		csvStream.GetVar(ref  m_UnitAttribute.m_moveSpeedMultiply); 
		csvStream.GetVar(ref  m_UnitAttribute.m_hp); 
		csvStream.GetVar(ref  m_UnitAttribute.m_hpAdd); 
		csvStream.GetVar(ref  m_UnitAttribute.m_hpMultiple);  
	}
}

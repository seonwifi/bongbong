using UnityEngine;
using System.Collections;

public class UnitAttribute 
{
	UnitLife m_HpMinMax;
	float    m_AttackDamgage;
	float    m_magicGage;

	public UnitAttribute()
	{
		m_HpMinMax = new UnitLife ();
		m_AttackDamgage = 10;
		m_magicGage = 10;
	}

	public float HP
	{
		get
		{
			return m_HpMinMax.HP;
		} 
		set
		{
			  m_HpMinMax.HP = value;
		}
	}
	public int HPInt
	{
		get
		{
			return (int)m_HpMinMax.HP;
		} 
	}
	public float HPMax
	{
		get
		{
			return m_HpMinMax.MaxHP;
		} 
	}
	public int HPIntMax
	{
		get
		{
			return (int)m_HpMinMax.MaxHP;
		} 
		set
		{
			m_HpMinMax.MaxHP = value;
		}
	}
}

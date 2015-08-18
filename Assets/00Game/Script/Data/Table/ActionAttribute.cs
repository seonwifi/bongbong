using UnityEngine;
using System.Collections;

public struct ActionAttribute 
{
	static public ActionAttribute Identity
	{
		get
		{
			ActionAttribute unitAttribute = new ActionAttribute ();
			unitAttribute.m_damage = 0;
			unitAttribute.m_damageAdd = 0;
			unitAttribute.m_damageMultiply = 0;
			
			unitAttribute.m_attackSpeed = 0;
			unitAttribute.m_attackSpeedAdd = 0;
			unitAttribute.m_attackSpeedMultiply = 0;
			
			unitAttribute.m_moveSpeed = 0;
			unitAttribute.m_moveSpeedAdd = 0;
			unitAttribute.m_moveSpeedMultiply = 0;
			
			unitAttribute.m_hp = 0;
			unitAttribute.m_hpAdd = 0;
			unitAttribute.m_hpMultiple = 0;

			return unitAttribute;
		}
	}

	public float 	m_damage;
	public float 	m_damageAdd;
	public float 	m_damageMultiply;

	public float 	m_attackSpeed;
	public float 	m_attackSpeedAdd;
	public float 	m_attackSpeedMultiply;

	public float    m_moveSpeed;
	public float    m_moveSpeedAdd;
	public float    m_moveSpeedMultiply;

	public float    m_hp;
	public 	float   m_hpAdd;
	public float    m_hpMultiple;
  
	static public ActionAttribute operator +(  ActionAttribute a, ActionAttribute b)
	{
		ActionAttribute unitAttribute 			= new ActionAttribute ();
		unitAttribute.m_damage         			 = a.m_damage     		+ b.m_damage;
		unitAttribute.m_damageAdd 				= a.m_damageAdd 		+ b.m_damageAdd;
		unitAttribute.m_damageMultiply  		= a.m_damageMultiply 	+ b.m_damageMultiply;

		unitAttribute.m_attackSpeed 			= a.m_attackSpeed 			+ b.m_attackSpeed;
		unitAttribute.m_attackSpeedAdd 			= a.m_attackSpeedAdd 		+ b.m_attackSpeedAdd;
		unitAttribute.m_attackSpeedMultiply 	= a.m_attackSpeedMultiply 	+ b.m_attackSpeedMultiply;

		unitAttribute.m_moveSpeed				= a.m_moveSpeed 			+ b.m_moveSpeed;
		unitAttribute.m_moveSpeedAdd 			= a.m_moveSpeedAdd 			+ b.m_moveSpeedAdd;
		unitAttribute.m_moveSpeedMultiply		= a.m_moveSpeedMultiply 	+ b.m_moveSpeedMultiply;

		unitAttribute.m_hp 						= a.m_hp 			+ b.m_hp;
		unitAttribute.m_hpAdd 					= a.m_hpAdd 		+ b.m_hpAdd;
		unitAttribute.m_hpMultiple 				= a.m_hpMultiple 	+ b.m_hpMultiple;
		return unitAttribute;
	}

	public void Add(ActionAttribute dest)
	{
		this = this + dest;
	}

	public float TotalDamage
	{
		get
		{
			return (m_damage*m_damageMultiply) + m_damageAdd;
		}
	}

	public float TotalAttackSpeed 
	{
		get
		{
			return (m_attackSpeed*m_attackSpeedMultiply) + m_attackSpeedAdd;
		}
	}

	public float TotalMoveSpeed
	{
		get
		{
			return (m_moveSpeed* m_moveSpeedMultiply) + m_moveSpeedAdd;
		}
	}

	public float TotalHp
	{
		get
		{
			return (m_hp + m_hpMultiple) + m_hpAdd;
		}
	}
}

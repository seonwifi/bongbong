using UnityEngine;
using System.Collections;

public partial class Unit : MonoBehaviour {

	// Use this for initialization
	void Ani_AttackBegin() 
	{
		if(m_ai.m_TargetUnit)
		{
			m_ai.m_TargetUnit.m_ai.SetDamage(this.GetSkillDamage()); 
		}
	}
	
	// Update is called once per frame
	void Ani_AttackEnd() 
	{
	
	}
}

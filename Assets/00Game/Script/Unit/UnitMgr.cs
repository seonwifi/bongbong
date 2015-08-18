using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eUnitType
{
	Armmy = 1,
	Enermy = 2,
}

public class UnitMgr  
{
	static UnitMgr m_ins = null;
	static public UnitMgr Ins
	{
		get
		{
			if(m_ins == null)
			{
				m_ins = new UnitMgr();
			}
			return m_ins;
		}
	}
 
	List<Unit> m_armmyUnitList = new List<Unit>();
	List<Unit> m_enermyUnitList = new List<Unit>();
	int m_FindClosestLoopCount = 0;
	Unit m_updateCurrentUnit = null;

	List<Unit> m_removeUnitList = new List<Unit>();
	int m_updateLoopCount = 0;

	public UnityEngine.Events.UnityAction<Unit> NewUnitCall = null;

	public void UpdateMgr()
	{
		m_updateLoopCount = m_armmyUnitList.Count;
		for(int i = 0;i < m_updateLoopCount; ++i)
		{
			m_updateCurrentUnit = m_armmyUnitList[i];
			if( m_updateCurrentUnit != null && m_updateCurrentUnit.m_ai != null)
			{
				m_updateCurrentUnit.m_ai.UpdateAI(); 
			} 
		}

		m_updateLoopCount = m_enermyUnitList.Count;
		for(int i = 0;i < m_updateLoopCount; ++i)
		{
			m_updateCurrentUnit = m_enermyUnitList[i];
			if(m_updateCurrentUnit != null && m_updateCurrentUnit.m_ai != null)
			{
				m_updateCurrentUnit.m_ai.UpdateAI(); 
			} 
		}
		m_updateCurrentUnit = null;

		m_updateLoopCount = m_removeUnitList.Count;
		if(m_updateLoopCount > 0)
		{
			for(int i = 0; i < m_updateLoopCount; ++i)
			{
				if(m_removeUnitList[i])
				{
					GameObject.Destroy(m_removeUnitList[i].gameObject);
				} 
			}
			m_removeUnitList.Clear();
		} 
	}

	public void UpdateHp()
	{
		m_updateLoopCount = m_armmyUnitList.Count;
		for(int i = 0;i < m_updateLoopCount; ++i)
		{
			m_updateCurrentUnit = m_armmyUnitList[i];
			if( m_updateCurrentUnit != null && m_updateCurrentUnit.m_ai != null)
			{
				m_updateCurrentUnit.UpdateHp();
			} 
		}
		
		m_updateLoopCount = m_enermyUnitList.Count;
		for(int i = 0;i < m_updateLoopCount; ++i)
		{
			m_updateCurrentUnit = m_enermyUnitList[i];
			if(m_updateCurrentUnit != null && m_updateCurrentUnit.m_ai != null)
			{
				m_updateCurrentUnit.UpdateHp();
			} 
		}


		m_updateCurrentUnit = null; 
	}

	public void RemoveUnit(Unit removeUnit)
	{
		if (removeUnit == null)
			return;
		if(removeUnit.m_ai.m_TeamType == eUnitType.Armmy)
		{
			for (int i = 0; i < m_armmyUnitList.Count; ++i)
			{
				if(m_armmyUnitList[i] == removeUnit)
				{
					if(m_armmyUnitList[i])
					{
						m_removeUnitList.Add(m_armmyUnitList[i]); 
					}
					m_armmyUnitList[i] = null; 
					break;
				}
			}
		}
 		else
		{
			for (int i = 0; i < m_enermyUnitList.Count; ++i)
			{
				if(m_enermyUnitList[i] == removeUnit)
				{
					if(m_enermyUnitList[i])
					{
						m_removeUnitList.Add(m_enermyUnitList[i]); 
					}
					m_enermyUnitList[i] = null; 
					break;
				}
			} 
		} 
	}

	public void AddArmmy(Unit addUnit)
	{
		bool b_set = false;

		addUnit.m_ai.m_TeamType = eUnitType.Armmy;
	
		for (int i = 0; i < m_armmyUnitList.Count; ++i)
		{
			if(m_armmyUnitList[i] == null)
			{
				m_armmyUnitList[i] = addUnit;
				b_set = true;
				break;
			}
		} 
		if(b_set == false)
		{
			m_armmyUnitList.Add(addUnit);
		}
		if(NewUnitCall != null)
		{
			NewUnitCall(addUnit);
		}
	}

	public void AddEnermy(Unit addUnit)
	{
		bool b_set = false;

		addUnit.m_ai.m_TeamType = eUnitType.Enermy;

		for (int i = 0; i < m_enermyUnitList.Count; ++i)
		{
			if(m_enermyUnitList[i] == null)
			{
				m_enermyUnitList[i] = addUnit;
				b_set = true;
				break;
			}
		} 
		if(b_set == false)
		{
			m_enermyUnitList.Add(addUnit);
		}

		if(NewUnitCall != null)
		{
			NewUnitCall(addUnit);
		}
	}

	public Unit FindCombatUnit(Vector3 centerPos, float sizeOfUnit,  float maxRange, eUnitType unitFindType, Unit expection)
	{ 
		Unit finedUnit = null;
		if(unitFindType == eUnitType.Armmy)
		{ 
			float zFrontPos 		= centerPos.z - sizeOfUnit;
			float closestLength 	= float.MaxValue; 
			float tempSqrMagnitude 	= 0;
			Unit tempUnit 			= null; 

			m_FindClosestLoopCount 	= m_armmyUnitList.Count;
			for(int i = 0; i < m_FindClosestLoopCount; ++i)
			{
				tempUnit = m_armmyUnitList[i];
				if(tempUnit != null &&  tempUnit.m_ai.m_dead == false &&  expection != tempUnit)
				{ 
					tempSqrMagnitude = zFrontPos - tempUnit.m_ai.FrontZ;

					if(tempSqrMagnitude <= maxRange)
					{ 
						tempSqrMagnitude =  zFrontPos - tempUnit.m_ai.FrontZ;
						if( 0 <= tempSqrMagnitude)
						{  
							if(closestLength >= tempSqrMagnitude )
							{
								closestLength = tempSqrMagnitude;
								finedUnit = tempUnit;
							} 
						}
						else
						{
							tempSqrMagnitude = (tempUnit.Position - centerPos).sqrMagnitude; 
							if(closestLength >= tempSqrMagnitude)
							{
								closestLength = tempSqrMagnitude;
								finedUnit = tempUnit;
							} 
						} 
					} 
				} 
			} 
		}
		
		else if(unitFindType == eUnitType.Enermy)
		{
			float zFrontPos 		= centerPos.z + sizeOfUnit;
			float closestLength 	= float.MaxValue;
			m_FindClosestLoopCount 	= m_enermyUnitList.Count;
			float tempSqrMagnitude 	= 0;
			Unit tempUnit 			= null; 

			for(int i = 0; i < m_FindClosestLoopCount; ++i)
			{
				tempUnit = m_enermyUnitList[i];
				if(tempUnit != null &&  tempUnit.m_ai.m_dead == false &&  expection != tempUnit)
				{
					tempSqrMagnitude = tempUnit.m_ai.FrontZ - zFrontPos;

					if(tempSqrMagnitude  <= maxRange)
					{
						if(0 <= tempSqrMagnitude)
						{ 
							if(closestLength >= tempSqrMagnitude)
							{
								closestLength = tempSqrMagnitude;
								finedUnit = tempUnit;
							} 
						}
						else
						{
							tempSqrMagnitude = (tempUnit.Position - centerPos).sqrMagnitude; 
							if(closestLength >= tempSqrMagnitude )
							{
								closestLength = tempSqrMagnitude;
								finedUnit = tempUnit;
							}  
						}
					}

				}  
			} 
		} 
		return finedUnit;
	}

	public Unit FindClosestUnit(Vector3 centerPos, float minRange, float maxRange, eUnitType unitFindType, Unit expection)
	{
		Unit finedUnit = null;
		if(unitFindType == eUnitType.Armmy)
		{ 
			float closestLength = float.MaxValue; 
			float tempSqrMagnitude = 0;
			Unit tempUnit = null;
			m_FindClosestLoopCount = m_armmyUnitList.Count;
			for(int i = 0; i < m_FindClosestLoopCount; ++i)
			{
				tempUnit = m_armmyUnitList[i];
				if(tempUnit != null &&  tempUnit.m_ai.m_dead == false &&  expection != tempUnit)
				{ 
					tempSqrMagnitude = (tempUnit.Position - centerPos).sqrMagnitude;
					if(closestLength >= tempSqrMagnitude && tempSqrMagnitude <= maxRange)
					{
						closestLength = tempSqrMagnitude;
						finedUnit = tempUnit;
					}
				}
			} 
		}

		else if(unitFindType == eUnitType.Enermy)
		{
			float closestLength = float.MaxValue;
			m_FindClosestLoopCount = m_enermyUnitList.Count;
			float tempSqrMagnitude = 0;
			Unit tempUnit = null;
			for(int i = 0; i < m_FindClosestLoopCount; ++i)
			{
				tempUnit = m_enermyUnitList[i];
				if(tempUnit != null &&  tempUnit.m_ai.m_dead == false &&  expection != tempUnit)
				{
					tempSqrMagnitude = (tempUnit.Position - centerPos).sqrMagnitude;
					if(closestLength >= tempSqrMagnitude && tempSqrMagnitude <= maxRange)
					{
						closestLength = tempSqrMagnitude;
						finedUnit = m_enermyUnitList[i];
					}
				}
			} 
		}
		else
		{
			float closestLength = float.MaxValue; 
			float tempSqrMagnitude = 0;
			Unit tempUnit = null;
			m_FindClosestLoopCount = m_armmyUnitList.Count;
			for(int i = 0; i < m_FindClosestLoopCount; ++i)
			{
				tempUnit = m_armmyUnitList[i];
				if(tempUnit != null &&  tempUnit.m_ai.m_dead == false &&  expection != tempUnit)
				{
					tempSqrMagnitude = (tempUnit.Position - centerPos).sqrMagnitude;
					if(closestLength >= tempSqrMagnitude && tempSqrMagnitude <= maxRange)
					{
						closestLength = tempSqrMagnitude;
						finedUnit = tempUnit;
					}
				}
			} 
 
			m_FindClosestLoopCount = m_enermyUnitList.Count; 
			for(int i = 0; i < m_FindClosestLoopCount; ++i)
			{
				tempUnit = m_enermyUnitList[i];
 
				if(tempUnit != null &&  tempUnit.m_ai.m_dead == false &&  expection != tempUnit)
				{
					tempSqrMagnitude = (tempUnit.Position - centerPos).sqrMagnitude;
					if(closestLength >= tempSqrMagnitude && tempSqrMagnitude <= maxRange)
					{
						closestLength = tempSqrMagnitude;
						finedUnit = m_enermyUnitList[i];
					}
				}
			} 
		}
		return finedUnit;
	}
}

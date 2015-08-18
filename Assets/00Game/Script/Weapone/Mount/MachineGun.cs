using UnityEngine;
using System.Collections;

public class GunData
{
	public float  	m_reloadTime 		= 0.5f;
	public int 		m_repeatCount 		= 1;
	public float    m_repeatTime		= 0.2f;
	public int		m_gunModelId 		= 0; 
}
public class BulletData
{ 
	public int		m_gunModelId 		= 0;
	public int		m_damage 			= 25;
	public float    m_bulletSpeed 		= 1;
}

public class MachineGun  : Mount
{ 
	public Transform []m_gunPoints;
	GunData 		m_GunData = null; 
	int 			m_fireCount = 0;
	BulletData 		m_bulletData;

	bool 			m_isFire = false;
	int  			m_startedFireCount = 0;
	float 	 		m_time = 0;
 
	eUnitType 		m_findType = eUnitType.Armmy; 

	void Awake()
	{
		this.enabled = false;
		Unit myUnit = this.gameObject.GetComponentInParent<Unit> ();
		if(myUnit)
		{
			myUnit.AddMount(this);
		}
		else
		{
			Debug.LogError("Error => NoHaveParent Unit");
		}
	}
	public override bool IsFire
	{
		get
		{
			return m_isFire;
		}
	}

	// Use this for initialization
	public override void StartFire (  eUnitType findType) 
	{ 
		m_findType = findType;
		m_GunData = new GunData ();
		m_bulletData = new BulletData ();
		m_fireCount = m_GunData.m_repeatCount; 
		//newBullet.Fire (); 
		m_isFire = true;
	}

	public override	void EndFire() 
	{
		m_isFire 	= false;
		m_time 		= 0; 
	}

 
	public override void UpdateFrame ()
	{
		if(m_isFire)
		{
			m_time -= Time.deltaTime;
			if(m_time <= 0)
			{ 
				for(int i = 0; i < m_gunPoints.Length; ++i)
				{
					Bullet newBullet 	= ResourceMgr.GetBullet(m_bulletData);  
					newBullet.Fire(m_bulletData, m_gunPoints[i].position, m_gunPoints[i].forward, m_findType);
				}
 
				--m_fireCount;
				if(m_fireCount <= 0)
				{
					m_time 		= m_GunData.m_reloadTime;
					m_fireCount = m_GunData.m_repeatCount;
				}
				else
				{
					m_time 		= m_GunData.m_reloadTime;
				}
			} 
		} 
	}
 
}








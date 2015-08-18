using UnityEngine;
using System.Collections;


//[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	public GameObject	m_BulletModel;
	public GameObject	m_crasheffect;
	BulletData 	m_BulletData;
	Rigidbody 	m_rigidbody;
	Transform 	MyTransform;
	eUnitType 	m_findUnitType 		= eUnitType.Armmy;
	float		m_baseSpeed 		= 25;
	bool		m_isFire 			= false;
	float 		m_maxRange 			= 40; 
 
	bool       m_destoryed = false;
	// Use this for initialization
	void Awake () 
	{
		m_rigidbody = this.gameObject.GetComponent<Rigidbody>();
		MyTransform = this.gameObject.GetComponent<Transform>();
	}
 
	public BulletData MyBulletData
	{
		get
		{
			return m_BulletData;
		}
		set
		{
			m_BulletData = value;
		}
	}
	

	// Update is called once per frame
	public void Fire(BulletData bulletData, Vector3 startPos, Vector3 velocityDir, eUnitType findUnitType) 
	{
		if(m_BulletModel) m_BulletModel.SetActive(true);
		if(m_crasheffect) m_crasheffect.SetActive(false); 
		m_destoryed 					= false; 
		m_findUnitType 					= findUnitType;
		float speed 					= m_baseSpeed * bulletData.m_bulletSpeed;
		m_rigidbody.transform.forward 	= velocityDir;
		m_rigidbody.position 			= startPos;
		m_rigidbody.velocity 			= velocityDir*speed;  

		CancelInvoke ();
		float endTime = m_maxRange/speed; 
		Invoke ("EndRange", endTime);
	}

	void EndRange() 
	{
		if(m_destoryed == false )
		{ 
			m_destoryed = true;
			GameObject.Destroy (this.gameObject);
		} 
	}

	GameObject m_tempGameObject = null;
	void OnTriggerEnter(Collider other)
	{
		if(m_destoryed)
		{
			return;
		}

		m_tempGameObject = other.gameObject;
		if(m_tempGameObject.layer == GameLayer.Unit)
		{
			Unit unit = m_tempGameObject.GetComponent<Unit>();
			if(unit != null && unit.m_ai.m_TeamType == m_findUnitType)
			{
				m_destoryed = true; 
				unit.m_ai.SetDamage(m_BulletData.m_damage);
				if(m_BulletModel) m_BulletModel.SetActive(false);
				if(m_crasheffect) m_crasheffect.SetActive(true);
				m_rigidbody.velocity = Vector3.zero;  
				GameObject.Destroy (this.gameObject, 2.0f);
			}
		}

	}
}

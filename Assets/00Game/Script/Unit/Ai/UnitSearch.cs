using UnityEngine;
using System.Collections;

public class UnitSearch : MonoBehaviour {
 
	public UnitAi m_myAi = null;
	GameObject m_closestObj = null;
	float      m_closestObjLength = float.MaxValue;
	//LinkedList<GameObject> m_enterList = new LinkedList<GameObject>();
	int m_searchLayer;

	public static UnitSearch CreateUnitSearch(Transform parentTr, float searchRange)
	{
		GameObject 		unitSearchObj   = new GameObject ("UnitSearch");
		unitSearchObj.transform.parent 	= parentTr;
		unitSearchObj.transform.localPosition = Vector3.zero; 
		unitSearchObj.layer = GameLayer.UnitSearch;
		SphereCollider 	sphereCollider 	= unitSearchObj.AddComponent<SphereCollider>();
		Rigidbody 		rigidbody		= unitSearchObj.AddComponent<Rigidbody> ();
		UnitSearch  	unitSearch 		= unitSearchObj.AddComponent<UnitSearch>();
		
		sphereCollider.radius			= searchRange;
		sphereCollider.isTrigger 		= true;
		rigidbody.isKinematic 			= true;
		rigidbody.useGravity 			= false; 
		unitSearchObj.SetActive (false);
		return unitSearch;
	}

	void Awake()
	{
		m_searchLayer = LayerMask.NameToLayer("Unit"); 
		this.gameObject.SetActive (false);
	}

	public void SetCallObj( )
	{
		
	}
	void OnEnable()
	{
 
	}

	void OnDisable()
	{
		m_closestObj = null;
		m_closestObjLength = float.MaxValue;
	}

	void OnTriggerEnter(Collider other)
	{  
		if(m_myAi == null)
		{
			return;
		}

		if (m_searchLayer == other.gameObject.layer) 
		{  
			if(m_myAi.m_unit.BodyCollyderObj == other.gameObject)
			{
				return;
			}

			if(m_closestObj == null)
			{
				m_closestObj = other.gameObject;
				m_closestObjLength = (m_myAi.m_unit.Position - other.transform.position).sqrMagnitude;
			}
			else if(m_closestObj != null)
			{
				if(other.transform != null)
				{
					float tempsqrLength = (m_myAi.m_unit.Position - other.transform.position).sqrMagnitude;
					if(m_closestObjLength >= tempsqrLength)
					{
						m_closestObj = other.gameObject;
						m_closestObjLength = m_closestObjLength;
					} 
				} 
			}
		} 
	}


	void Update()
	{
		if(m_myAi == null)
		{
			this.gameObject.SetActive (false);
			return;
		}
		if(m_closestObj != null)
		{
			//m_myAi.FindedClosetObj(m_closestObj);
			m_closestObj = null;
		}
	}
}

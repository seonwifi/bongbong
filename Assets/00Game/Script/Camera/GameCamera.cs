using UnityEngine;
using System.Collections;

[System.Serializable]
public class SmoothDampTween
{
	public float current; 
	public float target; 
	public float currentVelocity;
	public float smoothTime; 
	public float maxSpeed; 

	public SmoothDampTween()
	{
		Clear ();
	}

	public void Clear()
	{
		current = 0;
		target = 0;
		currentVelocity = 0;
		maxSpeed = 10;
		smoothTime = 0.13f;
	}

	public void Update (float deltaTime)
	{
		current = Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
	}

	public bool IsTarget
	{
		get
		{
			return Mathf.Abs(current - target) < 0.0001f ? true : false;
		}
	}
}

public class GameCamera : MonoBehaviour {
 
	static public GameCamera Ins = null;
	public Transform m_Transform = null;
	public Camera 	m_camera = null;
	public float     m_zOffset = 0;
	SmoothDampTween m_DampOption = new SmoothDampTween();

	UnitMgr m_uniMgr;
	Vector3 m_endTarget = Vector3.zero;
	Vector3 m_startPos = Vector3.zero;

	bool m_EnableFollowUnit = true;
	public bool EnableFollowUnit
	{
		get
		{
			return m_EnableFollowUnit;
		}
		set
		{

			m_EnableFollowUnit = value;
			if(m_EnableFollowUnit)
			{
				m_followDelayTime = 1.0f;
				m_dock = false;
			}
		}
	}
	void Awake ()
	{
		Ins = this;
	}

	// Use this for initialization
	void Init (Vector3 startPos)
	{
		m_startPos = startPos;
		Position = m_startPos; 
		m_uniMgr = GameMgr.Ins.m_unitMgr;
		m_endTarget = GameMgr.Ins.m_unitLocation.m_EnermyLocation.CommandCenter;

		m_DampOption.maxSpeed = 20;

	}

	void OnDestroy()
	{
		Ins = null;
		m_uniMgr = null;
	}

	void Start()
	{
		 
	}

	public Vector3 Position
	{
		get
		{
			return m_pos;
		}
		set
		{ 
			m_pos = value;
			m_Transform.position = m_pos;
		}
	}
	public float PositionZ
	{
		get
		{
			return m_pos.z;
		}
		set
		{ 
			m_pos.z = value;
			m_Transform.position = m_pos;
		}
	}

	Vector3 m_pos = Vector3.zero;

	Unit m_currentUnit = null;
	bool m_dock = true;
	float m_followDelayTime = 0.5f;
	// Update is called once per frame
	public void UpdatePos () 
	{ 
		if(m_uniMgr == null)
		{
			Init ( GameMgr.Ins.m_unitLocation.m_cameraStartPos.position);
		}


		if(EnableFollowUnit )
		{
			m_followDelayTime -= Time.deltaTime;
			if(m_followDelayTime <= 0)
			{
				Unit l_unit = m_uniMgr.FindClosestUnit(m_endTarget, 0, 10000, eUnitType.Armmy, null);
				
				if(m_currentUnit != l_unit)
				{
					m_dock = false;
					m_currentUnit = l_unit;
					
				} 
				
				if(m_dock == false)
				{
					m_dock = UpdateFollowTarget ();
				}
				else
				{
					if(m_currentUnit != null)
					{ 
						PositionZ = m_currentUnit.Position.z + m_zOffset;
					}
				}
			}

		} 
	}



	bool UpdateFollowTarget () 
	{
		if(m_currentUnit == null)
		{
			m_DampOption.target = m_startPos.z + m_zOffset;
		}
 		else 
		{ 
			m_DampOption.target = m_currentUnit.Position.z+ m_zOffset;
		}
		m_DampOption.current = m_pos.z;
		m_DampOption.Update ( Time.smoothDeltaTime);

		PositionZ = m_DampOption.current; 
		return m_DampOption.IsTarget;
	}
}

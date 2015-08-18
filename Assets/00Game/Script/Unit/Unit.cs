using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Unit : MonoBehaviour
{

	public enum eUnitState
	{
		Empty 	= 0,
		Idle,
		Moving,
		Attack,
		Dmage,
		Max,
	}

	public enum eMovingType
	{
		None = 0,
		Vector,
		Unint,
	}  
	public UxUnitGagebar						m_unitGagebar; 
	public NavMeshAgent 						m_NavMeshAgent; 
	public SkillData							m_currentSkill = null;
	public Vector3 								m_Movingtarget;
	public GameObject 							m_BodyCollyderObj;
	public Rigidbody							m_rigidbody; 
	public float								m_hpYOffset = 2.5f;
	public GameObject							[]m_DieOnActives;
	//public Unit									m_targetUnit;
	//public UnitAttribute 						m_UnitAttribute;
	[HideInInspector] public ActorManager	 	m_ActorManager;
	[HideInInspector] public int 				m_cacheId = 0;
	[HideInInspector] public int			 	m_EnermyId = 0;
	[HideInInspector] public Transform 			MyTransform;  
	[HideInInspector] public UnitAi				m_ai;

	float       								m_DestAgentGap = 0.15f;

	ObstacleAvoidanceType 						m_NavQuality = ObstacleAvoidanceType.LowQualityObstacleAvoidance; 
	bool 										m_AutoAttack   = false;
	bool 										m_attackEnable = false;
	bool 										m_movingEnable = true;   
	eMovingType 								m_eMovingType;  
	bool 										called = false;
	Animator									m_animator;
	public MeshRenderer								m_mat;
	float  m_UnitSize = 0;

	[HideInInspector] public List<Mount> m_mountList = new List<Mount> ();

	public bool AttackEnable
	{
		get
		{
			return m_attackEnable;
		}
		set
		{
			m_attackEnable = value;
		}
	}
	
	public bool AutoAttack
	{
		get
		{
			return m_AutoAttack;
		}
		set
		{
			m_AutoAttack = value;
			AttackEnable = true;
			EnableMoving = true;
		}
	}
	
	public bool EnableMoving
	{
		get
		{
			return m_movingEnable;
		}
		set
		{
			m_movingEnable = value;
		}
	}

	public bool EnableNavAgent
	{
		get
		{
			if(m_NavMeshAgent)
			{
				return m_NavMeshAgent.enabled;
			}
			return false;
		}
		set
		{
			if(m_NavMeshAgent)
			{
				m_NavMeshAgent.enabled = value;
			} 
		}
	}
 
	public Vector3 Position
	{	
		get
		{
			return MyTransform.position;
		}
		set
		{
			MyTransform.position = value;
		}
	}

	public float  UnitSize
	{
		get
		{ 
			return  m_UnitSize;
		}
		set
		{
			m_UnitSize = value; 
		}
	}

	public float  AttackRange
	{
		get
		{
			if(m_currentSkill == null)
			{
				return 10.5f;
			}
			return m_currentSkill.attack_range;
		}
	}

	public GameObject BodyCollyderObj
	{
		get
		{
			return m_BodyCollyderObj;
		}
	}

	void OnDestroy ()
	{
		m_mountList.Clear ();
	}

 	void Awake ()
	{  
		MyTransform = this.transform;
		m_animator = this.gameObject.GetComponent<Animator>();  
 
		CapsuleCollider capsuleCollider = this.gameObject.GetComponent<CapsuleCollider> ();
		if(capsuleCollider != null)
		{
			m_UnitSize = capsuleCollider.radius;
		}

		SphereCollider sphereCollider = this.gameObject.GetComponent<SphereCollider> ();
		if(sphereCollider != null)
		{
			m_UnitSize = sphereCollider.radius;
		} 

		if(m_DieOnActives != null)
		{
			int l_loop = m_DieOnActives.Length;
			for(int i = 0; i < l_loop; ++i)
			{
				m_DieOnActives[i].SetActive(false);
			}
		}

	}

	void SetDeadOnActive()
	{
		if(m_DieOnActives != null)
		{
			int l_loop = m_DieOnActives.Length;
			for(int i = 0; i < l_loop; ++i)
			{
				m_DieOnActives[i].SetActive(true);
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
		CreateGagebar ();

		m_SkillDataList.Add (new SkillData ());


		if(m_ai.m_TeamType == eUnitType.Armmy)
		{
			//m_mat.material.color = new Color(0,0,1,1);
			m_unitGagebar.GagebarColor = new Color(0,0,1,1);
		}
		else
		{
			//m_mat.material.color = new Color(1,0.2f,0.2f,1);
			m_unitGagebar.GagebarColor = new Color(1,0.2f,0.2f,1);
		}
	}
 
 

	void CreateGagebar()
	{
		if(m_unitGagebar)
		{
			Transform parent = m_unitGagebar.transform.parent;
			m_unitGagebar = (GameObject.Instantiate(m_unitGagebar.gameObject) as GameObject).GetComponent<UxUnitGagebar>();
			m_unitGagebar.gameObject.SetActive(true);  
			(m_unitGagebar.transform as RectTransform).SetParent(parent);
 
			m_unitGagebar.transform.localScale = new Vector3(1,1,1);
			
		}
	}
  
 
//	void LateUpdate () 
//	{
//		UpdateHp ();
//	}
	public void UpdateHp () 
	{
		if(m_unitGagebar && m_ai != null && m_ai.m_UnitAttribute != null)
		{ 
			m_unitGagebar.UpdateHp(Camera.main, MyTransform.position,  m_ai.m_UnitAttribute.HPInt, m_ai.m_UnitAttribute.HPIntMax, m_hpYOffset);
		}
	}
 

	public void SetNavQuality(ObstacleAvoidanceType v_NavQuality)
	{
		m_NavQuality = v_NavQuality;
		if(m_NavMeshAgent) m_NavMeshAgent.obstacleAvoidanceType = m_NavQuality;
	}
 
 
	public float GetSkillDamage()
	{
		if(m_currentSkill == null)
		{
			return  0;
		}

		return  m_currentSkill.damage;
	}

	//목적지 까지 도착 했는지 확인...
	float m_endTargetMin = 0.01f;
	public bool IsMovingEndOfTarget()
	{ 
		if(m_NavMeshAgent == null)
		{
			return true;
		}
 

		float stopDist = m_NavMeshAgent.stoppingDistance + m_endTargetMin;
		//m_NavMeshAgent
		if((m_NavMeshAgent.pathEndPosition - this.Position).sqrMagnitude < stopDist*stopDist)
		{
			return true;
		}
		return false;
	}
 

	bool m_isMove = false;
	public void Move(Vector3 v_target)
	{
		if(!m_movingEnable)
		{
			return;
		}

		if(m_NavMeshAgent)
		{
			if(EnableNavAgent == false)
			{
				EnableNavAgent = true;
			}
			m_isMove = true;
			m_NavMeshAgent.stoppingDistance = 9;
			m_NavMeshAgent.Resume();
			m_NavMeshAgent.SetDestination(v_target); 
		} 
		MotionChange(ref m_moveMotionName, 0.25f, 0, 0);
	}

	public void MoveFly(Vector3 v_target)
	{
		Vector3 dir = v_target - Position;
		dir.Normalize ();
		dir *= 5;
		m_rigidbody.velocity = dir;
	}

	public  void StopMove()
	{ 
		if(m_NavMeshAgent && m_NavMeshAgent.isActiveAndEnabled)
		{
			m_isMove = false;
			m_NavMeshAgent.Stop();
		} 
	}

	public  bool IsMove()
	{
		return m_isMove;
	}

	List<SkillData> m_SkillDataList = new List<SkillData>();
	bool m_isAttack = false;
	float m_attackStartTime = 0;
	float m_attackTime = 0;
	public bool IsAttacking()
	{
		if (!m_isAttack)
			return false;

		if(Time.time > m_attackStartTime + m_attackTime)
		{
			return false;
		} 
		return true;
	}
 
	public bool MotionChange(ref string v_Name, float transitionDuration, int layer, float normalizedTime)
	{
		if (!m_animator)
			return false;
		if(m_animator) m_animator.CrossFade( v_Name,  transitionDuration, layer, normalizedTime);

		return true;
	}

	 
	string m_moveMotionName = "run";
	string m_idleMotionName = "idle";
	string m_dieMotionName = "die";

	public bool Attack(int v_id)
	{ 
		SkillData selectSkill = null;
		if(v_id < m_SkillDataList.Count)
		{
			selectSkill = m_SkillDataList[v_id];
		}
		
		if(selectSkill != null)
		{ 
			m_isAttack = true;
			MotionChange(ref selectSkill.motion_name, 0.25f, 0, 0);
			m_currentSkill = selectSkill;
			m_attackStartTime = Time.time;
			m_attackTime   = selectSkill.attack_time;
		}
		else
		{
			m_attackStartTime = Time.time;
			m_attackTime = 0;
			return false;
		}
		return true;
	}

	public bool Idle()
	{  
		MotionChange(ref m_idleMotionName, 0.25f, 0, 0);
		return true;
	}

	public bool Die()
	{  
		SetDeadOnActive ();
		MotionChange(ref m_dieMotionName, 0.25f, 0, 0);
		return true;
	}


	public void LookTarget(Vector3 target)
	{
		target = target - Position;
		target.y = 0; 
		MyTransform.forward = target;

	}
 

	public void AddMount(Mount newMount)
	{
		m_mountList.Add (newMount);
	}
}
 






using UnityEngine;
using System.Collections;

public class FlyUnitAgent : IUpdate
{
	bool m_isEnable = false;
	Vector3 m_beforPos = Vector3.zero;
	Vector3		m_target = Vector3.zero;

	//public  	Rigidbody 	m_rigidBody;
	public 		Unit		m_unit;
	public		bool 		m_isMove = false;
	public 		float		m_speed  = 3.0f;
	public		float		m_rotateMaxSpeed = Mathf.PI;
	public		float		m_rotateSpeedCurrent =  Mathf.PI;

	public Vector3 TargetMove
	{
		set
		{ 
			m_target = value;
 
			m_beforPos 				= m_unit.Position;

			Vector3 targetVector 	= (m_target - m_unit.Position);

			float lengthTarget 		= targetVector.sqrMagnitude;

			if(lengthTarget < 0.0001f*0.0001f)
			{
				m_unit.Position = m_target;
				m_isMove = false;
			}
			else
			{ 
				targetVector.Normalize();
				TargetFoward = targetVector; 
				m_isMove = true;
			}
		}
	}
	Vector3 m_targetFoward = new Vector3();

	public virtual void UpdateFrame ()
	{
		if(m_isMove)
		{
			m_unit.Position = Vector3.MoveTowards( m_unit.Position, m_target, m_speed*Time.deltaTime);
			
			Vector3  beforToTarget 		=  m_target - m_beforPos;
			Vector3  currentToTarget 	=  m_target - m_unit.Position; 
			float dot = Vector3.Dot (beforToTarget, currentToTarget);
			if(dot <= 0)
			{ 
				//m_rigidBody.velocity = Vector3.zero;
				m_unit.Position = m_target;
			}
			else
			{
				float lengthTarget = currentToTarget.sqrMagnitude;
				if(lengthTarget > 0.0001f*0.0001f)
				{
					//m_unit.MyTransform.forward 	= currentToTarget;
				}
				
			}

		}


		{
			float dot = Vector3.Dot (m_unit.MyTransform.forward, m_targetFoward);
			if( dot < 0.998f )
			{
				// The step size is equal to speed times frame time.
				var step = m_rotateSpeedCurrent * Time.deltaTime;
				
				Vector3 newForward = Vector3.RotateTowards(m_unit.MyTransform.forward, m_targetFoward, step, 0.0f); 
				
				// Move our position a step closer to the target.
				m_unit.MyTransform.rotation = Quaternion.LookRotation(newForward);
				
			} 
			else
			{
				m_unit.MyTransform.rotation = Quaternion.LookRotation(m_targetFoward); 
			}
		}

	}

	public Vector3 TargetFoward
	{
		set
		{
			m_targetFoward = value;
			m_targetFoward.Normalize();
			//m_unit.MyTransform.forward 	= value;
		}
		get
		{
			return m_targetFoward;
		}
	}

	public Vector3 CurrentFoward
	{
		get
		{
			return m_unit.MyTransform.forward;
		}
	}

	public bool IsCurrentFowardTargetFoward
	{
		get
		{
			float dot = Vector3.Dot (m_unit.MyTransform.forward, m_targetFoward);
			if( dot > 0.998f )
			{
				return true;
			}

			return false;
		}
	}

	public virtual 	bool enableUpdate 
	{
		get
		{
			return m_isEnable;
		}
		set
		{
			m_isEnable = value;
		}
	}

	public void StopMove()
	{
		m_isMove = false;
		//if(m_rigidBody != null)m_rigidBody.velocity = Vector3.zero;
	}
}


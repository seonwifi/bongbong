using UnityEngine;
using System.Collections;

public class GameTeamLocation : MonoBehaviour {

	public Transform 	m_LandStartPosRoot;  
	public Transform	m_SkyPosRoot;   
	public Transform  	m_FinalDestination;  
	public Transform  	m_CommandCenter; 

	// Use this for initialization
	void Start ()
	{
		this.enabled = false;
		this.gameObject.SetActive (false);	
	}
 
	public Vector3 FinalDestination
	{
		get
		{
			return m_FinalDestination.position;
		}
	}
	public Vector3 CommandCenter
	{
		get
		{
			return m_CommandCenter.position;
		}
	}

	public int LandStartPosCount
	{
		get
		{
			if(m_LandStartPosRoot == null) return 0;

			return m_LandStartPosRoot.childCount;
		}
	}

	public int SkyStartPosCount
	{
		get
		{
			if(m_SkyPosRoot == null) return 0;
			
			return m_SkyPosRoot.childCount;
		}
	}

	public Vector3 GetLandStartPos( int i)
	{
		return GetStartPos (m_LandStartPosRoot, i);
	}

	public Vector3 GetSkyStartPos( int i)
	{
		return GetStartPos (m_SkyPosRoot, i);
	}
	
	public Vector3 GetStartPos(Transform tr, int i)
	{
		if(i >= tr.childCount)
		{
			Debug.LogError("Array Over Max StartPos => " + tr.childCount);
			return tr.position;
		}
		
		return tr.GetChild (i).position;
	}
#if UNITY_EDITOR
	void OnDrawGizmos() 
	{
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawWireCube(transform.position, new Vector3(1,1,1));
		 
		//GizmosDrawSphere(Color.yellow, 1, m_LandStartPosRoot);
		//GizmosDrawSphere(Color.yellow, 1, m_SkyPosRoot);
		GizmosDrawSphere(Color.yellow, 1, m_FinalDestination);
		GizmosDrawSphere(Color.yellow, 1, m_CommandCenter); ; 

		for(int i = 0; i < LandStartPosCount; ++i)
		{
			GizmosDrawSphere( Color.green, 0.5f, GetLandStartPos(i));
		}

		for(int i = 0; i < SkyStartPosCount; ++i)
		{
			GizmosDrawSphere( Color.blue, 0.5f,GetSkyStartPos(i));
		}
	}

	void GizmosDrawSphere( Color color, float size, Transform tr) 
	{
		if(tr == null)return; 
		Gizmos.color = color;
		Gizmos.DrawSphere(tr.position,size); 
	}
	void GizmosDrawSphere( Color color, float size, Vector3 pos) 
	{  
		Gizmos.color = color;
		Gizmos.DrawSphere(pos,size); 
	}
#endif
}

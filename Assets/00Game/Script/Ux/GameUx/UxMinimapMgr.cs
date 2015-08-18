using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UxMinimap : System.IDisposable
{
	Unit 					m_unit = null;
	UnityEngine.UI.Image 	m_image = null;
	bool 				 	m_empty = true;

	virtual public void Dispose ()
	{
		m_unit = null;
		m_image = null;
	}


	public bool Empty
	{
		get
		{
			return m_empty;
		} 
	}

	public void SetEmpty()
	{
		m_empty = true;
		m_unit = null; 
		m_image.gameObject.SetActive (false);
	}

	public void SetMinimap(UnityEngine.UI.Image minimapImage)
	{
		m_image = minimapImage;
	}

	public void SetUnit(Unit unit)
	{
		m_unit = unit;
		if(m_unit != null && m_unit.m_ai.m_dead == false)
		{
			m_empty = false;
			if(unit.m_ai.m_TeamType == eUnitType.Armmy)
			{
				m_image.color = Color.blue;
			}
			else
			{
				m_image.color = Color.red;
			}
			m_image.gameObject.SetActive (true);
		}
	}

	public void Update(float width, float height)
	{
		if (m_empty)
			return;
		if(m_unit != null && m_unit.m_ai.m_dead == false)
		{
			Vector3 pos01 = GameMgr.Ins.GetMapToNomalPos(m_unit.Position);
			pos01.x *= width;
			pos01.y *= height;
			pos01.z = 0;
			m_image.rectTransform.anchoredPosition3D = pos01; 
		}
		else
		{
			SetEmpty();
		}
	}
}

public class UxMinimapMgr : System.IDisposable
{ 
	GameObject m_prefabMinimapUint = null;

	List<UxMinimap> m_minimapUnitList = new List<UxMinimap> ();
	Image			m_bg;
	int m_tempLoopCount = 0;
	bool m_isDispose = false;
	virtual public void Dispose ()
	{
		m_isDispose = true;
		m_prefabMinimapUint 	= null;
		m_bg				 	= null;
		for(int i = 0; i < m_minimapUnitList.Count; ++i)
		{
			m_minimapUnitList[i].Dispose();
		}
		m_minimapUnitList.Clear ();
	}


	UxMinimap GetNewMinimap (Unit newUnit)
	{
		UxMinimap newMinimap = null;
		m_tempLoopCount = m_minimapUnitList.Count;
		for(int i = 0; i < m_tempLoopCount; ++i)
		{
			if(m_minimapUnitList[i].Empty)
			{
				newMinimap = m_minimapUnitList[i];
				break;
			}
		}
		if(newMinimap == null)
		{
			newMinimap 							= new UxMinimap();
			GameObject l_GameObject 			= GameObject.Instantiate<GameObject> (m_prefabMinimapUint);
			Image image 						= l_GameObject.GetComponent<Image>();
 
			image.rectTransform.SetParent(l_GameObject.transform.parent);
			newMinimap.SetMinimap(image); 
			m_minimapUnitList.Add(newMinimap);
		}
		if(newMinimap != null) newMinimap.SetUnit(newUnit);
 
		return newMinimap;
	}

	void NewUnit (Unit newUnit)
	{
		if (m_isDispose)
			return;
		GetNewMinimap (newUnit);  

	}

	// Use this for initialization
	public void Init ( Image bg, GameObject prefabMinimapUint)
	{
		m_bg 					= bg; 
		m_prefabMinimapUint 	= prefabMinimapUint;

		GameMgr.Ins.m_unitMgr.NewUnitCall += NewUnit;
	}
	
	// Update is called once per frame
	public void Update ()
	{
		int loopCount =  m_minimapUnitList.Count;
		for(int i = 0; i < loopCount; ++i)
		{
			m_minimapUnitList[i].Update(m_bg.rectTransform.rect.width, m_bg.rectTransform.rect.height);
		}
	}
}

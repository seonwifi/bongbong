using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NativeUpdater : MonoBehaviour 
{
	static NativeUpdater _Ins = null;
	public static NativeUpdater Ins
	{
		get
		{
			if(_Ins == null)
			{
				_Ins = (new GameObject("NativeUpdater")).AddComponent<NativeUpdater>();
				_Ins.gameObject.isStatic = true;
			}
			return _Ins;
		}
	}
 
	public class LayerUpdate
	{
		public UpdateMgr  m_UpdateMgr = new UpdateMgr();
	}
	List<LayerUpdate> m_LayerUpdateList = new List<LayerUpdate>();
	int m_tempCount = 0;
	// Use this for initialization
	void Start () 
	{
	
	}
	void AddUpdate (int layerUpdate, IUpdate newUpdate) 
	{
		if(layerUpdate >= m_LayerUpdateList.Count)
		{
			int addCount = (layerUpdate - m_LayerUpdateList.Count )+ 1;
			for(int i = 0; i < addCount; ++i)
			{
				m_LayerUpdateList.Add(null);
			}
		}

		if(m_LayerUpdateList[layerUpdate] == null)
		{
			m_LayerUpdateList[layerUpdate] = new LayerUpdate();
		}

		m_LayerUpdateList[layerUpdate].m_UpdateMgr.Add(newUpdate);
	}

	// Update is called once per frame
	void Update ()
	{
		m_tempCount = m_LayerUpdateList.Count;
		for(int i = 0; i < m_tempCount; ++i)
		{
			if(m_LayerUpdateList[i] != null)
			{
				m_LayerUpdateList[i].m_UpdateMgr.Update();
			}
		}
	}

 
	void OnDestroy ()
	{
		for(int i = 0; i < m_LayerUpdateList.Count; ++i)
		{
			if(m_LayerUpdateList[i] != null)
			{
				m_LayerUpdateList[i].m_UpdateMgr.Dispose();
				m_LayerUpdateList[i].m_UpdateMgr = null;
				m_LayerUpdateList[i] = null;
			} 
		}
		m_LayerUpdateList.Clear();
	}
}

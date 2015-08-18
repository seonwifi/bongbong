using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class UpdateMgr : System.IDisposable
{
	List<IUpdate> m_updateList = new List<IUpdate>();

	int 		m_tempCount = 0;
	IUpdate 	m_tempUpdate = null;

	public virtual void Dispose ()
	{
		m_updateList.Clear (); 
		m_updateList = null;
		m_tempCount = 0;
		m_tempUpdate = null;
	}

	// Update is called once per frame
	public void Update ()
	{
		if (m_updateList == null)
			return;

		m_tempCount = m_updateList.Count;

		for(int i = 0; i < m_tempCount; ++i)
		{
			m_tempUpdate = m_updateList[i];
			if(m_tempUpdate == null)
			{
				m_updateList[i] = null;
				continue;
			}
				 
			if(m_tempUpdate.enableUpdate)
			{
				m_tempUpdate.UpdateFrame();
			} 
		}

		m_tempUpdate = null;
	}

	public void Add (IUpdate newUpdate)
	{
		if (m_updateList == null)
			return;
#if UNITY_EDITOR
		if(m_updateList.Contains(newUpdate))
		{
			Debug.LogError("UpdateMgr => Add Contain Update");
		}
#endif
		bool findNull 	= false;
		m_tempCount 	= m_updateList.Count;
		for(int i = 0; i < m_tempCount; ++i)
		{
			if(m_updateList[i] == null)
			{
				m_updateList[i] = newUpdate;
				findNull = true;
				break;
			}
		}

		if(findNull == false)
		{
			m_updateList.Add (newUpdate);
		}
	}

	public void Remove (IUpdate removeUpdate)
	{
		if (m_updateList == null)
			return;
		if (removeUpdate == null)
			return;

		m_tempCount = m_updateList.Count;
		for(int i = 0; i < m_tempCount; ++i)
		{
			if(m_updateList[i] == removeUpdate)
			{
				m_updateList[i] = null;
				break;
			}
		}
	}

	public T GetUpdateClass<T> () where T : class, IUpdate
	{
		m_tempCount = m_updateList.Count;
		for(int i = 0; i < m_tempCount; ++i)
		{
			if(m_updateList[i] != null)
			{ 
				T asClass = m_updateList[i] as T;
				if(asClass != null)
				{
					return asClass;
				}
			}
		}

		return default(T);
	} 
}


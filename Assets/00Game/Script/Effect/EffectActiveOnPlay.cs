using UnityEngine;
using System.Collections;

public class EffectActiveOnPlay : MonoBehaviour 
{

	public GameObject m_effectPrefab;
	// Use this for initialization
	protected virtual void OnEnable () 
	{
		if(m_effectPrefab)
		{
			GameObject.Instantiate(m_effectPrefab, this.transform.position, this.transform.rotation);
		}
	}
}

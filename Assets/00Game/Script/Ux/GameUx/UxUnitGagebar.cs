using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UxUnitGagebar : MonoBehaviour 
{
	public Image 	m_Image_GageAfer;
	public Image 	m_Image_Gage;
 
	int 			m_MaxHp 				= 0;
	int  			m_lastMaxHp  			= int.MinValue;
	float	 		m_MaxHpPercent 			= 0;
	float 			m_lastHp  				= float.MinValue;
	float 			m_value 				= 1;
	Vector3 		m_LastScreenPos 		= Vector3.zero; 
	RectTransform 	m_rectTransform			= null;

	// Use this for initialization
	public float Value
	{
		set
		{
			m_value = value;

			if(m_Image_GageAfer)	m_Image_GageAfer.fillAmount = m_value;
			if(m_Image_Gage) 		m_Image_Gage.fillAmount 	= m_value;
		
		}
		get
		{
			return m_value;
		}
	}
 
	public Color GagebarColor
	{
		set
		{
			m_Image_Gage.color = value;
		}
	}
 
 
	void Awake()
	{
		m_rectTransform = this.gameObject.GetComponent<RectTransform>(); 
	}

	public void UpdateHp(Camera gameCamera, Vector3 pos, float currentHp, int  maxHp, float yOffset)
	{
		if(m_lastHp != currentHp || m_lastMaxHp != maxHp)
		{
			//hp
			if(m_MaxHp != maxHp)
			{
				m_MaxHp 		= maxHp;
				m_MaxHpPercent 	= 1/(float)m_MaxHp;
			}

			m_value = currentHp*m_MaxHpPercent; 

			if(m_Image_GageAfer)	m_Image_GageAfer.fillAmount = m_value;
			if(m_Image_Gage) 		m_Image_Gage.fillAmount 	= m_value;
		}
  
		//position
		pos.y 			+= yOffset; 
		m_LastScreenPos = pos;
		m_rectTransform.OtherWorldPosToPos_Lq( null, pos, gameCamera);  
		  
	}
}

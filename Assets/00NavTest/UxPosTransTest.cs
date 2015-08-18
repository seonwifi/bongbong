using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UxPosTransTest : MonoBehaviour {

	public Camera m_cam;
	public RectTransform m_rectTransform;
	public Canvas 		 m_canvas;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateHp();
	}

	public void UpdateHp()
	{
  
		Vector3 scrPos = m_cam.WorldToScreenPoint (this.transform.position);
		Vector2 Vector2ScrPos = new Vector2(scrPos.x, scrPos.y);
		 
	 
		//if(scrPos != m_LastScreenPos)
//		{
// 
//			Vector2 Vector2ScrPos = new Vector2(scrPos.x, scrPos.y);
//			
//			
//			//Debug.Log (rectTransform.ToString());
//			Vector2 localPos;
//			
//			if(m_rectTransform) m_rectTransform.position = RectTransformUtility.PixelAdjustPoint (scrPos, m_rectTransform, m_canvas);
//		}
//		

		m_rectTransform.ScreenToPosition_Lq(Vector2ScrPos, null);
		 
//			Vector2 localPos;
//		if(RectTransformUtility.ScreenPointToLocalPointInRectangle( m_canvas.transform as RectTransform, Vector2ScrPos,  null, out localPos))
//		{
//			m_rectTransform.localPosition = localPos;
//		}
		
		//		RectTransform
		//		UnityEngine.Canvas c;
		
//		154 		Vector2 localCursor; 
//		155 		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle (m_ViewRect, data.position, data.pressEventCamera, out localCursor)) 
//			156 			return; 
//		157 		 

	}
}

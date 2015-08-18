using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameInput : MaskableGraphic, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, 
IDropHandler, IEndDragHandler , IPointerDownHandler
{

	Plane m_cameraDragPlane = new Plane( new Vector3(0,1,0), new Vector3(0,0,0));

	public virtual  void OnInitializePotentialDrag (PointerEventData eventData)
	{
//		UnityEngine.EventSystems.EventSystem A;
//		A.
		Debug.Log ("OnInitializePotentialDrag");
	}
	bool 	m_dragCamera 	= false;
	Vector3 m_beforTouchPos = Vector3.zero;

	public virtual void OnBeginDrag (PointerEventData eventData)
	{
		Debug.Log ("<color=blue>OnBeginDrag</color>");
		DebugLogPointerEventData (eventData); 

		if(GameCamera.Ins)
		{
			m_beforTouchPos = Input.mousePosition;
			GameCamera.Ins.EnableFollowUnit = false;
			m_dragCamera = true;
		}
		else
		{
			m_dragCamera = false;
		}
	} 

	public virtual  void OnDrag (PointerEventData eventData)
	{
		 
		Debug.Log ("OnDrag");
		DebugLogPointerEventData (eventData); 

	}

	public void Update()
	{
		if(m_dragCamera == true && GameCamera.Ins != null)
		{
			Ray beforRay = GameCamera.Ins.m_camera.ScreenPointToRay(m_beforTouchPos);
			float enter = 0;
			if(m_cameraDragPlane.Raycast(beforRay, out enter))
			{
				Vector3 beforPos = beforRay.origin + beforRay.direction*enter;
				Vector3 currentTouchPos = Input.mousePosition; 
				Ray ray = GameCamera.Ins.m_camera.ScreenPointToRay(currentTouchPos);
				
				if(m_cameraDragPlane.Raycast(ray, out enter))
				{
					Vector3 currentPos = ray.origin + ray.direction*enter;
					Vector3 Offset = beforPos - currentPos;
					Offset.x = 0;
					Vector3 currentCameraPosition = GameCamera.Ins.Position;
					currentCameraPosition += Offset;
					GameCamera.Ins.Position = currentCameraPosition;
 
					m_beforTouchPos = Input.mousePosition;
				}
			} 
		}
	}

	public virtual  void OnDrop (PointerEventData eventData)
	{
		Debug.Log ("OnDrop");
		DebugLogPointerEventData (eventData); 
	}

	public virtual  void OnEndDrag (PointerEventData eventData)
	{
		Debug.Log ("OnEndDrag");
		DebugLogPointerEventData (eventData); 

		if(GameCamera.Ins)
		{
			GameCamera.Ins.EnableFollowUnit = true;
		}

		m_dragCamera = false;
	}

	public virtual  void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log ("<color=red>OnPointerDown</color>");
		DebugLogPointerEventData (eventData); 
	}
	
	public virtual  void DebugLogPointerEventData (PointerEventData eventData)
	{
		Debug.Log ("eventData.pointerId => " + eventData.pointerId);
		Debug.Log ("eventData.pressPosition  => " + eventData.pressPosition.ToString ());
		Debug.Log ("eventData.position => " + eventData.position.ToString());
	}

//
//	#region	multi touch  
//	//multi touch begin
//	enum eTouchControllMode
//	{
//		Anithing 	= 0,
//		Moving,
//		Zoom,
//	}
//	Camera 		m_mainCamera;  
//	Vector3   	m_dragSlidePos 					= new Vector3(0,0,0);
//	Vector3   	m_dragSlideNormal 				= new Vector3(0,1,0);
//	float		m_movingThreashRangePixel 		= 40;//pixel
//	float		m_movingThreashRangeCm 			= 0.5f;//0.356f;//dpi->cm
//	Plane 		m_dragSlidePlane 				= new Plane(new Vector3(0,1,0), new Vector3(0,0,0)); 
//	
//	int  	m_fingerIdTouch0 	= -1; 
//	int  	m_fingerIdTouch1 	= -1;  
//	Vector2 m_lastTouchPos0 	= new Vector2(0,0);
//	Vector2 m_lastTouchPos1 	= new Vector2(0,0);	
//	
//	Vector2 m_fristTouchPos0 	= new Vector2(0,0);
//	Vector2 m_fristTouchPos1 	= new Vector2(0,0);
//	Vector3 m_firstTouchPos 	= new Vector3(0,0,0);
//	float 	m_firstTouchZoom 	= 0;
//	
//	bool    m_bOnMultiTouch 	= false;
//	
//	float 	m_currentZoom 		= 0;
//	float 	m_currentZoomVelo 	= 0;	
//	float 	m_targetZoom	 	= 0;	
//	float 	m_currentZoomOffsetScalePixel 	= 0.063f;//pixel
//	float 	m_currentZoomOffsetScaleCm   	= 0.3f;//dpi->cm
//	float   m_movingFactor 		= -1;
//	
//	eTouchControllMode m_touchControllMode = eTouchControllMode.Anithing;
//	bool m_isMovingedCamera = false;
//	bool m_cameraPosLerped = false;
//	#if UNITY_EDITOR || UNITY_STANDALONE_OSX
//	bool m_mouseLDown = false; 
//	Vector3 m_mouseDownPos = Vector3.zero; 
//	#endif
//	
//	//one touch move begin
//	bool	m_bOneTouchBegin			= false;
//	Vector2 m_fristOneTouchPos 			= new Vector2(0,0);
//	Vector2 m_lastOneTouchPos 			= new Vector2(0,0);
//	bool	m_bOneTouchMovingCamera 	= false;
//
//	//one touch move end
//	void Awake () 
//	{
//		_ins = this;
//	}
//	
//	bool 	mIsPress = false;
//	
//	void OnEnable () 
//	{
//		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
//		m_mouseLDown   = false;  
//		#endif
//	}
//	
//	void OnDisable () 
//	{
//		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
//		m_mouseLDown   = false;  
//		#endif
//	}
//	
//	void Update()
//	{
//		UpdateTouch();
//	}
//	
//	bool UpdateTouch(Camera cam) 
//	{   
//		m_mainCamera   = cam;
//		float m_maxOffsetPos = 8;
//		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
//		if(true)
//		{ 
//			float scrollWheelZoom = 0;
//			if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
//			{
//				scrollWheelZoom = -0.25f;
//			}
//			if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
//			{
//				scrollWheelZoom = 0.25f;
//			} 
//			
//			//SmCamera.ins.Zoom += scrollWheelZoom;
//			if(Input.GetMouseButtonDown(0))
//			{
//				m_mouseLDown   = true;  
//				m_mouseDownPos = Input.mousePosition;
//			}
//			if(Input.GetMouseButtonUp(0))
//			{
//				m_mouseLDown = false; 
//			}
//			
////			if(m_mouseLDown)
////			{  
////				float l_RotationX = (18.5f*Time.smoothDeltaTime);
////				float l_RotationY = (18.5f*Time.smoothDeltaTime);
////				
////				Vector3 offset = Input.mousePosition - m_mouseDownPos;
////				offset.x *= l_RotationX;
////				offset.y *= l_RotationY;
////				SmCamera.ins.SetRotateOffset(offset.y, offset.x);
////				
////				m_mouseDownPos = Input.mousePosition;
////			}
//			
//			//return false;
//		}
//		
//		#endif		
//		/*
//  		if(Input.touchCount < 2)
//		{
//			m_fingerIdTouch0 = -1;  
//			m_fingerIdTouch1 = -1;  
//			m_bOnMultiTouch = false;
//			return false;
//		}//*/
//		
//		if(!Input.multiTouchEnabled)
//		{
//			m_fingerIdTouch0 = -1;
//			m_fingerIdTouch1 = -1;
//			if(m_isMovingedCamera == true)
//			{  
//				m_isMovingedCamera = false;
//			} 
//			return false;
//		}
//		
//		int touchThreash = 0*0;
//		int touchCount = Input.touchCount;  
//		UnityEngine.Touch []touches = Input.touches; 
//		
//		if(touchCount == 1)
//		{
//			float CPI = 2.54f; 
//			Touch touch0 = touches[0]; 
//			if(touch0.phase == TouchPhase.Began)
//			{  
//				m_bOneTouchBegin	= true;
//				m_fristOneTouchPos 	= touch0.position;
//				m_lastOneTouchPos   = m_fristOneTouchPos;  
//			}
//			else if(touch0.phase == TouchPhase.Moved)
//			{
//				if(m_bOneTouchBegin)
//				{
//					float l_dragLength = (m_fristOneTouchPos - touch0.position).SqrMagnitude();
//					//
//					if(l_dragLength > touchThreash) 
//					{  
//						if(m_bOneTouchMovingCamera == false)
//						{
//							m_bOneTouchMovingCamera = true;  
//						} 
//					} 
//				} 
//			}
//			else if(touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled)
//			{ 
//				m_bOneTouchMovingCamera = false;
//			}
//			
//			if(m_bOneTouchMovingCamera == true && m_bOneTouchBegin == true)
//			{
//				Vector2 l_touch0Position   	= touch0.position; 
//				
//				float l_RotationX = (1500*Time.smoothDeltaTime);
//				float l_RotationY = (1500*Time.smoothDeltaTime);
//				
//				Vector3 offset = l_touch0Position - m_lastOneTouchPos;
//				offset.x = PixelToCench(offset.x);
//				offset.y = PixelToCench(offset.y);
//				offset.x *= l_RotationX;
//				offset.y *= l_RotationY;
//				
//				//SmCamera.ins.SetRotateOffset(offset.y, offset.x);
//				
//				
//				m_lastOneTouchPos		 	= touch0.position;
//			}
//			else if( m_bOneTouchBegin == true)
//			{
//				m_lastOneTouchPos		 	= touch0.position;
//			}
//			
//			if(m_bOneTouchMovingCamera)
//			{
//				return true;
//			}
//			else
//			{
//				return false;
//			}
//		}
//		else
//		{   
//			m_bOneTouchBegin = false;
//			if(touchCount == 0)
//			{
//				if(m_bOneTouchMovingCamera == true)
//				{ 
//					m_bOneTouchMovingCamera = false;
//				}
//			} 
//		}//*/
//		
//		
//		
//		bool reCooldFirstPos = false;
//		for (int i = 0; i < touchCount; i++ )
//		{
//			UnityEngine.Touch touch = touches[ i ];
//			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
//			{
//				if(m_fingerIdTouch0 == -1 && m_fingerIdTouch1 != touch.fingerId)
//				{ 
//					m_fingerIdTouch0	= touch.fingerId;  
//					reCooldFirstPos 	= true; 					
//				}
//				else if(m_fingerIdTouch1 == -1  && m_fingerIdTouch0 != touch.fingerId)
//				{
//					m_fingerIdTouch1    = touch.fingerId;  
//					reCooldFirstPos 	= true;
//				}
//			}
//			else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
//			{
//				if(m_fingerIdTouch0 == touch.fingerId)
//				{
//					m_fingerIdTouch0 = -1;  
//				}
//				if(m_fingerIdTouch1 == touch.fingerId)
//				{
//					m_fingerIdTouch1 = -1;  
//				}
//			} 
//		} 
//		
//		if(reCooldFirstPos == true)
//		{
//			m_touchControllMode = eTouchControllMode.Zoom;
//			//m_touchControllMode = eTouchControllMode.Anithing;
//			m_targetZoom        = m_firstTouchZoom 	= m_currentZoom = SmCamera.ins.Zoom;
//			for (int i = 0; i < touchCount; i++ )
//			{
//				UnityEngine.Touch touch = touches[ i ]; 
//				if(m_fingerIdTouch0 == touch.fingerId)
//				{ 
//					m_lastTouchPos0     = touch.position;
//					m_fristTouchPos0 	= touch.position; 
//				}
//				else if(m_fingerIdTouch1 == touch.fingerId)
//				{
//					m_lastTouchPos1     = touch.position;
//					m_fristTouchPos1	= touch.position; 
//				} 
//			} 
//		}
//		
//		if(m_fingerIdTouch0 != -1 && m_fingerIdTouch1 != -1 && touchCount >= 2)
//		{
//			//twoTouch Down
//			if(!m_bOnMultiTouch)
//			{
//				//m_dragState 	= eDragState.None;
//				//m_dragTarget 	= eDragTarget.None;		
//				//cameraPosLerp state backup  
//			} 
//			m_bOnMultiTouch = true;
//		}
//		else
//		{
//			//twoTouch End
//			if(m_bOnMultiTouch)
//			{
//				if(m_cameraPosLerped == true || m_isMovingedCamera == true)
//				{ 
//					m_isMovingedCamera = false;
//				}
//				m_bOnMultiTouch = false;
//			} 
//		}
//		
//		if(m_bOnMultiTouch == true)
//		{
//			float CPI = 2.54f; 
//			Touch touch0 = touches[m_fingerIdTouch0];
//			Touch touch1 = touches[m_fingerIdTouch1];
//			if(touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
//			{ 
//				if(m_touchControllMode == eTouchControllMode.Anithing)
//				{
//					
//					Vector2 movingDir0 = (touch0.position - m_fristTouchPos0);
//					Vector2 movingDir1 = (touch1.position - m_fristTouchPos1);
//					float   movingLength0 = movingDir0.magnitude;
//					float   movingLength1 = movingDir1.magnitude; 
//					float   movingLength2 = movingLength0 + movingLength1; 
//					
//					float movingThreashRange = m_movingThreashRangePixel;
//					if(Screen.dpi != 0)
//					{
//						movingThreashRange = m_movingThreashRangeCm;
//						movingLength2	   = (movingLength2/Screen.dpi)*CPI;
//					}
//					if(  movingLength2 > movingThreashRange)
//					{
//						movingDir0.Normalize();
//						movingDir1.Normalize();
//						float movingDot = Vector2.Dot(movingDir0, movingDir1);
//						if(movingDot > 0.0f)
//						{
//							m_touchControllMode = eTouchControllMode.Moving;
//						}
//						else
//						{
//							m_touchControllMode = eTouchControllMode.Zoom;
//						}	
//					}
//				}//*/
//				Vector2 beforPos0 			= m_lastTouchPos0;
//				Vector2 beforPos1 			= m_lastTouchPos1;
//				Vector2 center2Touch 		= ( touch1.position + touch0.position ) * 0.5f;
//				Vector2 lastCenter2Touch	= ( beforPos1 + beforPos0 ) * 0.5f;
//				m_lastTouchPos0				= touch0.position;
//				m_lastTouchPos1				= touch1.position;
// 
//				//moving 
//				if(m_touchControllMode == eTouchControllMode.Anithing || m_touchControllMode == eTouchControllMode.Moving)
//				{
//					
//					m_dragSlidePlane.SetNormalAndPosition(m_dragSlideNormal, m_dragSlidePos); 
//					Vector3 vScreenCurrent 	= new Vector3(center2Touch.x, center2Touch.y, 1);
//					Ray ray 				= m_mainCamera.ScreenPointToRay(vScreenCurrent); 
//					float enter 			= 0;
//					if(m_dragSlidePlane.Raycast(ray, out enter) == true)
//					{
//						vScreenCurrent		= ray.origin + ray.direction*enter; 
//						Vector3 vScreenLast = new Vector3(lastCenter2Touch.x, lastCenter2Touch.y, 1);
//						ray 				= m_mainCamera.ScreenPointToRay(vScreenLast); 	
//						if(m_dragSlidePlane.Raycast(ray, out enter) == true)
//						{
//							vScreenLast 	 = ray.origin + ray.direction*enter; 
//							Vector3 offset   = (vScreenCurrent  - vScreenLast)*m_movingFactor; 
//							
//							WoSv.Ins.m_playerCameraTrans.m_positionOffset += offset; 
//							if(WoSv.Ins.m_playerCameraTrans.m_positionOffset.magnitude > m_maxOffsetPos)
//							{
//								WoSv.Ins.m_playerCameraTrans.m_positionOffset.Normalize();
//								WoSv.Ins.m_playerCameraTrans.m_positionOffset *= m_maxOffsetPos;
//							}
//							m_isMovingedCamera = true; 
//						} 
//					} 
//				}
//				else
//				{
//					
//					if(WoSv.Ins.m_playerCameraTrans.m_cameraPosLerp == false)
//					{
//						WoSv.Ins.m_playerCameraTrans.RestoreCameraPos(Vector3.zero);
//					} 
//					
//				}//*/
//				
//				
//				//zoom
//				if(m_touchControllMode == eTouchControllMode.Anithing || m_touchControllMode == eTouchControllMode.Zoom)
//				{ 
//					float distanceCurrent  		= Vector2.Distance( touch1.position, touch0.position);
//					float distanceLast    		= Vector2.Distance( beforPos1, beforPos0);
//					float distanceOffSet   		= distanceLast - distanceCurrent;
//					if(Screen.dpi == 0)
//					{
//						m_targetZoom				+= distanceOffSet*m_currentZoomOffsetScalePixel; 
//					}
//					else
//					{ 
//						float distanceOffSetInch    = distanceOffSet/Screen.dpi;
//						float distanceOffSetCm      = distanceOffSetInch*CPI;
//						m_targetZoom				+= distanceOffSetCm*m_currentZoomOffsetScaleCm; 
//					} 
//					m_currentZoom = m_targetZoom;
//					//m_currentZoom   			= Mathf.SmoothDamp(m_currentZoom, m_targetZoom, ref m_currentZoomVelo, 0.005f);   
//				}
//				else
//				{
//					m_currentZoom = m_targetZoom;
//					//m_currentZoom   			= Mathf.SmoothDamp(m_currentZoom, m_firstTouchZoom, ref m_currentZoomVelo, 0.005f);  
//				}
//				//SmCamera.ins.Zoom  = m_currentZoom;
//			}
//			
//			return true;
//		} 
//		
//		return false;
//	}		
//	//multi touch end
//	#endregion
//
//	static float inchToCm = 2.54f;
//	float m_touchDragLengthScreen = 0;
// 
//	
//	static public float PixelToCench(float pixel)
//	{
//		#if UNITY_EDITOR
//		return pixel/42.9184f;
//		#else
//		return (pixel/(float)Screen.dpi) * inchToCm;
//		#endif
//	}
}

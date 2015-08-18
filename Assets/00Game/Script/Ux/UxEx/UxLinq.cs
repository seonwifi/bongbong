using UnityEngine;
using System.Collections;
using System.Linq;

public static class UxLinq  
{ 
	public static bool ScreenToPosition_Lq(this UnityEngine.UI.Graphic graphic, Vector2 screenPos)
	{
		if(graphic == null)
		{
			Debug.LogError("ScreenToPosition_Lq if(graphic == null)");
			return false;
		}

		bool result = false;
		RectTransform partenRT = graphic.rectTransform.parent.transform as RectTransform;
		if(partenRT == null)
		{
			Debug.LogError("ScreenToPosition_Lq if(partenRT == null)");
			return false;
		}
			

		Vector2 localPos = Vector2.zero;
		Camera cam = null;
		if( graphic.canvas)
		{
			cam = graphic.canvas.worldCamera;
		} 
		result = RectTransformUtility.ScreenPointToLocalPointInRectangle( partenRT, screenPos,  cam, out localPos);
		if(result)
		{
			graphic.rectTransform.localPosition = localPos; 
		}
		return result;
	}
	public static bool OtherWorldPosToPos_Lq(this UnityEngine.UI.Graphic graphic,  Vector3 worldPos, Camera worldPosCam)
	{ 
		if(graphic == null)
		{
			Debug.LogError("OtherWorldPosToPos_Lq if(graphic == null)");
			return false;
		}

		if(worldPosCam == null)
		{
			Debug.LogError("OtherWorldPosToPos_Lq if(worldPosCam == null)");
			return false;
		}
		worldPos = worldPosCam.WorldToScreenPoint(worldPos); 
		return graphic.ScreenToPosition_Lq(new Vector2(worldPos.x, worldPos.y)); 
	}

	public static bool ScreenToPosition_Lq(this RectTransform rt, Vector2 screenPos, Camera uiCam)
	{
		if(rt == null)
		{
			Debug.LogError("ScreenToPosition_Lq if(rt == null)");
			return false;
		}

		bool result = false;
		RectTransform partenRT = rt.parent.transform as RectTransform;
		if(partenRT == null)
		{
			//System.Diagnostics.Debug.Assert(
			Debug.LogError("ScreenToPosition_Lq if(partenRT == null)");
			return false;
		}
			
		Vector2 localPos = Vector2.zero;
		result = RectTransformUtility.ScreenPointToLocalPointInRectangle(partenRT, screenPos, uiCam, out localPos); 
		if(result)
		{
			rt.localPosition = localPos; 
		}
		return result;
	}

	public static bool OtherWorldPosToPos_Lq(this RectTransform rt, Camera uiCam, Vector3 worldPos, Camera worldPosCam)
	{
		if(rt == null)
		{
			Debug.LogError("OtherWorldPosToPos_Lq if(rt == null)");
			return false;
		}

		if(worldPosCam == null)
		{
			Debug.LogError("OtherWorldPosToPos_Lq if(worldPosCam == null)");
			return false;
		}
		worldPos = worldPosCam.WorldToScreenPoint(worldPos); 
		return rt.ScreenToPosition_Lq( worldPos, uiCam); 
	}


}

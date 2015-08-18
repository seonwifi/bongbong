using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameTeamLocation))]
[CanEditMultipleObjects]
public class GameTeamLocationEditor : Editor 
{

	public override void OnInspectorGUI() 
	{
		base.OnInspectorGUI ();

	}
	// Use this for initialization
	void OnSceneGUI ()
	{
		GameTeamLocation gameTeamLocation = this.target as GameTeamLocation;
		if(gameTeamLocation == null)
		{
			return;
		}

		for(int i = 0; i < gameTeamLocation.LandStartPosCount; ++i)
		{ 
			Handles.Label (gameTeamLocation.GetLandStartPos(i), new GUIContent(string.Format("Land Pos {0}", i)));
		}

		for(int i = 0; i < gameTeamLocation.SkyStartPosCount; ++i)
		{ 
			Handles.Label (gameTeamLocation.GetSkyStartPos(i), new GUIContent(string.Format("sky Pos {0}", i)));
		}

		if(gameTeamLocation.m_CommandCenter != null)
		{
			Handles.Label (gameTeamLocation.CommandCenter, new GUIContent("Command Center")); 
		}

	}
	
 
}

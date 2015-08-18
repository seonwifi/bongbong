using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UxText))]
[CanEditMultipleObjects]
public class UxTextEditor : Editor 
{
	[MenuItem("GameObject/UI/Ux/UxText")]
	static void CreateText()
	{
		if(Selection.activeGameObject != null)
		{
			Canvas canvas = Selection.activeGameObject.GetComponentInParent<Canvas>();

			if(canvas != null)
			{
				UxText newText 				= (new GameObject("UxText")).AddComponent<UxText>(); 

				newText.transform.parent 	= Selection.activeGameObject.transform;

				Selection.activeGameObject 	= newText.gameObject;
			}
		}
	}

	// Use this for initialization
	public override void OnInspectorGUI() 
	{
		base.OnInspectorGUI ();

		UxText uxText = this.target as UxText;
 
		if(GUILayout.Button (""))
		{
			 
		}
	}
	
 
}

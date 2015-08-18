using UnityEngine;
using System.Collections;

public class TMonoBehaviour : MonoBehaviour {

	[HideInInspector] public Transform MyTransform;
 
	protected virtual void Awake ()
	{
		MyTransform = this.transform; 
	} 
 
 
}

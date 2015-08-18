using UnityEngine;
using System.Collections;

public class EffectPool : MonoBehaviour 
{ 
	// Use this for initialization
	protected virtual void OnEnable () 
	{
		GameObject.Destroy (this.gameObject, 2.0f);
	}
}

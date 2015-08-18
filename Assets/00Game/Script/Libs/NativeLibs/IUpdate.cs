using UnityEngine;
using System.Collections;

public interface IUpdate 
{ 
	void UpdateFrame ();  
//	void BeginUpdate ();  
//	void DisableUpdate ();  
//	void EnableUpdate ();  
	bool enableUpdate 
	{
		get;
		set;
	}
}

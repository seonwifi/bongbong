using UnityEngine;
using System.Collections;
 
public class GameMapSetting : MonoBehaviour 
{
	public GameTeamLocation m_ArrmyLocation;
	public GameTeamLocation m_EnermyLocation; 
	public GameObject m_gagebar;

	public Transform  m_mapLeft;
	public Transform  m_mapRight;

	public Transform m_cameraStartPos;
	// Use this for initialization
	void Awake ()
	{ 
		GameMgr.Ins.StartGame (this);
		this.gameObject.SetActive (false);
	}
}

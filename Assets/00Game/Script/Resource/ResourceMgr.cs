using UnityEngine;
using System.Collections;

public class ResourceMgr 
{

	// Use this for initialization
 
	
	// Update is called once per frame
	static public Bullet GetBullet(BulletData bulletData)
	{
		GameObject bulletObj = Resources.Load<GameObject>("Bullet/Bullet");
		bulletObj = GameObject.Instantiate (bulletObj);
		Bullet bullet = bulletObj.GetComponent<Bullet>();
		bullet.MyBulletData = bulletData;
		return bullet;
	}
}

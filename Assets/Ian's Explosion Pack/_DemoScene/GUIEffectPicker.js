var FX : Transform[];

function OnGUI ()
 {
	for(var i = 0; i < FX.length; i ++)
	{
		if(GUI.Button(Rect(120*i,0,120,80), FX[i].gameObject.name))
		{
		var ap : ExplosionAtPoint = null;
			ap = gameObject.GetComponent(typeof("ExplosionAtPoint"));
			ap.explosionPrefab = FX[i];
		}
	}
	Time.timeScale = GUI.HorizontalSlider(Rect(0, 130, Screen.width, 10), Time.timeScale, 0, 15);
}
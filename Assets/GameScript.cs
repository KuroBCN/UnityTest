using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : Singleton<GameScript> {

	public GoogleStaticMap map;
	public int zoom = 10;
	protected GameScript() {}	// guarantee this will be always a singleton only - can't use the constructor!

	void Awake (){
		Time.timeScale = 1;
	}

	// Use this for initialization
	void Start () {

		GeoPoint qumramGeolocation = deviceLocationScript.Instance.loc;

		map.initialize ();
		map.centerMercator = map.tileCenterMercator (qumramGeolocation);
		map.zoom = zoom;
		map.DrawMap ();

		map.transform.localScale = Vector3.Scale (
			new Vector3 (map.mapRectangle.getWidthMeters (), map.mapRectangle.getHeightMeters (), 1.0f),
			new Vector3 (map.realWorldtoUnityWorldScale.x, map.realWorldtoUnityWorldScale.y, 1.0f));

	}
	
	// Update is called once per frame
	void Update () {

//		if (playerStatus == PlayerStatus.TiedToDevice) {
		GeoPoint newPosition = deviceLocationScript.Instance.loc;
		var tileCenterMercator = map.tileCenterMercator(newPosition);
		map.centerLatLon = newPosition;

		if( !map.centerMercator.isEqual(tileCenterMercator) || map.zoom != zoom ) {

			//newMap.SetActive(true);
			map.centerMercator = tileCenterMercator;
			map.zoom = zoom;		
			map.DrawMap ();

			/*
			getNewMapMap ().transform.localScale = Vector3.Scale(
				new Vector3 (getNewMapMap ().mapRectangle.getWidthMeters (), getNewMapMap ().mapRectangle.getHeightMeters (), 1.0f),
				new Vector3(getNewMapMap ().realWorldtoUnityWorldScale.x, getNewMapMap ().realWorldtoUnityWorldScale.y, 1.0f));	

			Vector2 tempPosition = GameManager.Instance.getMainMapMap ().getPositionOnMap (getNewMapMap ().centerLatLon);
			newMap.transform.position = new Vector3 (tempPosition.x, 0, tempPosition.y);

			GameObject temp = newMap;
			newMap = mainMap;
			mainMap = temp;
			*/
		}
	}
}

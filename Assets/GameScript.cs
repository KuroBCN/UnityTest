using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : Singleton<GameScript> {

	public GoogleStaticMap map;
	public GameObject myPlayer;
	public int zoom = 15;
	public float lat = 41.395996f;
	public float lon = 2.190890f;

	void Awake (){
		Time.timeScale = 1;
	}

	// Use this for initialization
	void Start () {
		map.initialize ();
		GeoPoint qumramGeolocation = new GeoPoint (41.395996f, 2.190890f);
		map.centerMercator = map.tileCenterMercator (qumramGeolocation);
		map.zoom = zoom;
		map.DrawMap ();

//		yield return StartCoroutine (locationService._StartLocationService ());
//		StartCoroutine (locationService.RunLocationService ());
	}
	
	// Update is called once per frame
	void Update () {

//		if (playerStatus == PlayerStatus.TiedToDevice) {
		GeoPoint newPosition = new GeoPoint(lat,lon);

		var tileCenterMercator = map.tileCenterMercator(newPosition);
		map.centerLatLon = newPosition;
		if(!map.centerMercator.isEqual(tileCenterMercator) || map.zoom != zoom) {

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

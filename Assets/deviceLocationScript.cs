using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deviceLocationScript : MonoBehaviour {


	public GeoPoint loc = new GeoPoint();
	private double lastLocUpdate = 0.0; //seconds
	public int maxWait = 30; // seconds
	private float locationUpdateInterval = 0.2f; // seconds

	public float trueHeading;

	// Use this for initialization
	IEnumerator Start () {
		yield return StartCoroutine (_StartLocationService ());
		StartCoroutine (RunLocationService ());		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public IEnumerator _StartLocationService()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser) {
			Debug.Log ("Locations is not enabled.");

			//NOTE: If location is not enabled, we initialize the postion of the player to somewhere in Los Angeles, just for demonstration purposes
			loc.setLatLon_deg (41.395996f, 2.190890f); 

			// To get the game run on Editor without location services
//			locServiceIsRunning = true;
			yield break;
		}

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in maxWait seconds
		if (maxWait < 1)
		{
			print("Locations services timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Location services failed");
			yield break;
		} else if (Input.location.status == LocationServiceStatus.Running){
			loc.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
			Debug.Log ("Location: " + Input.location.lastData.latitude.ToString ("R") + " " + Input.location.lastData.longitude.ToString ("R") + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			lastLocUpdate = Input.location.lastData.timestamp;
		} else {
			print ("Unknown Error!");
		}
		Debug.Log (loc.ToString());
	}

	public IEnumerator RunLocationService()
	{
		double lastLocUpdate = 0.0;
		while (true) {
			if (lastLocUpdate != Input.location.lastData.timestamp) {
				loc.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
				trueHeading = Input.compass.trueHeading;
				Debug.Log ("Location: " + Input.location.lastData.latitude.ToString ("R") + " " + Input.location.lastData.longitude.ToString ("R") + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
				//locServiceIsRunning = true;
				lastLocUpdate = Input.location.lastData.timestamp;
			}
			yield return new WaitForSeconds(locationUpdateInterval);
		}
	}
	void OnGUI() {
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10, 10, 100, 20),string.Format("{0} {1}", loc.lat_d, loc.lon_d));
//		if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
//			print("You clicked the button!");
	}
}

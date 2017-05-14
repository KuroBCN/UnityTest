using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharScript : MonoBehaviour {


	public float lat = 41.395996f;
	public float lon = 2.190890f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		setPositionOnMap();

		if (!Input.location.isEnabledByUser) {
//			float x = transform.position.x + Input.GetAxis ("Horizontal") * 0.3f * Time.deltaTime;
//			float z = transform.position.z + Input.GetAxis ("Vertical") * 0.3f * Time.deltaTime;
//			transform.position = new Vector3 (x, 0.05f, z); 
		}
	}

	public void setPositionOnMap() {
		GeoPoint pos = new GeoPoint(lat,lon);
		Vector2 tempPosition = GameScript.Instance.map.getPositionOnMap (pos);
		transform.position = new Vector3 (tempPosition.x, transform.position.y, tempPosition.y);
	}
}

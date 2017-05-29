using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		setPositionOnMap();
	}

	public void setPositionOnMap() {
		GeoPoint pos = deviceLocationScript.Instance.loc;
		Vector2 tempPosition = GameScript.Instance.map.getPositionOnMap (pos);
		transform.position = new Vector3 (tempPosition.x, transform.position.y, tempPosition.y);
	}
		
}

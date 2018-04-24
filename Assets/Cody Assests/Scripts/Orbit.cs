using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

	public GameObject Planet;// The gameobject that will orbit around
	public float speed;// speed of orbiting 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		OrbitAround ();
	}

	void OrbitAround()
	{
		transform.RotateAround (Planet.transform.position, Vector3.up, speed * Time.deltaTime);
	}
}

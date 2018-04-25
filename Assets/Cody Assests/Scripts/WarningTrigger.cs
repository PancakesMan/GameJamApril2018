using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTrigger : MonoBehaviour {

	public GameObject WarningText;


	// Use this for initialization
	void Start () 
	{
		WarningText.SetActive (false);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
			WarningText.SetActive (true);
	}
}


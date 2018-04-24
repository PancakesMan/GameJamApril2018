using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public Transform target;
    public float FollowingDistance = 10.0f;
    public float FloatingDistance = 0.0f;
    public float Speed = 1.0f;
	
	// Update is called once per frame
	void Update () {
        transform.forward = Vector3.MoveTowards(transform.forward, target.forward, Time.deltaTime * Speed);
        transform.position = target.position - FollowingDistance * transform.forward - transform.up * FloatingDistance;
	}
}

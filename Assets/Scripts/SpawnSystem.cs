using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour {

    public GameObject Spawn;
    public List<Transform> SpawnPositions;
    public float SpawnCooldown = 1.0f;
    private float timer = 0.0f;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > SpawnCooldown)
            Instantiate(Spawn, SpawnPositions[Random.Range(0, SpawnPositions.Count)]);
	}
}

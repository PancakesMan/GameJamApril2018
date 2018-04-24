using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour {

    public List<GameObject> Spawns;
    public List<Transform> SpawnPositions;
    public float SpawnCooldown = 1.0f;
    private float timer = 0.0f;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > SpawnCooldown)
        {
            Instantiate(Spawns[Random.Range(0, Spawns.Count)], SpawnPositions[Random.Range(0, SpawnPositions.Count)]);
            timer = 0.0f;
        }
	}
}

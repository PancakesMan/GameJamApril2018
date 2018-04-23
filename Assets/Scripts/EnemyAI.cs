using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float Speed;
    public float AttackCooldown = 1.0f;
    private float timer = 0.0f;

    GameObject Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Player.transform);
        timer += Time.deltaTime;
        if (timer > AttackCooldown)
        {
            timer = 0.0f;
            // Use an attack
        }
	}

    private void FixedUpdate()
    {
        transform.position += transform.forward * Speed * Time.fixedDeltaTime;
    }
}

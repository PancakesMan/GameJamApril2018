﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int ScoreValue = 100;
    public float Speed;
    public float AttackCooldown = 1.0f;
    public int AttackDamage = 1;
    public float AttackDistance = 10.0f;
    public ParticleSystem Attack;
    public List<Transform> AttackPositions;

    public ParticleSystem DeathExplosion;

    float timer = 0.0f;
    GameObject Player;
    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Player.transform);
        timer += Time.deltaTime;
        if (timer > AttackCooldown && Vector3.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            timer = 0.0f;
            foreach (var position in AttackPositions)
            {
                ParticleSystem ps = Instantiate(Attack, position);
                ps.transform.parent = transform;
            }
        }
	}

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * Speed * Time.deltaTime * (Vector3.Distance(transform.position, Player.transform.position) < 40 ? 0 : 1);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Player.GetComponent<ScoreSystem>().Score += ScoreValue;
            Instantiate(DeathExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

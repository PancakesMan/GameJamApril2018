using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int ScoreValue = 100;
    public float Speed;
    public float AttackCooldown = 1.0f;
    public ParticleSystem Attack;
    public List<Transform> AttackPositions;

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
        if (timer > AttackCooldown)
        {
            timer = 0.0f;
            foreach (var position in AttackPositions)
                Instantiate(Attack, position);
        }
	}

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * Speed * Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Player.GetComponent<ScoreSystem>().Score += ScoreValue;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserAI : MonoBehaviour {

    public int AttackDamage = 1;
    public float AttackDistance = 10.0f;
    public ParticleSystem Attack;
    public List<Transform> AttackPositions;

    GameObject Player;
    float timer = 0.0f;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        foreach (var position in AttackPositions)
        {
            position.LookAt(Player.transform);
            if (Vector3.Distance(position.position, Player.transform.position) < AttackDistance && timer > 0.1f)
            {
                timer = 0.0f;
                ParticleSystem ps = Instantiate(Attack, position);
                ps.transform.parent = transform;
            }
        }
	}
}

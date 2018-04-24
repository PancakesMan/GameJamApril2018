using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float BaseSpeed, SoftSpeedCap, HardSpeedCap, CurrentSpeed, BoostSpeedModifier;
    public float RotateSpeed = 1.0f;
    public ParticleSystem Attack;
    public List<Transform> AttackPositions;
    public float AttackCooldown = 1.0f;

    public ParticleSystem DeathExplosion;

    private float timer = 0.0f;
    private bool Boosted = false;
    private HealthSystem HP;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        HP = GetComponent<HealthSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        // Toggle speed boost when either shift is pressed
        if (CurrentSpeed >= SoftSpeedCap &&
            Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.RightShift))
                Boosted = !Boosted;

        // Shoot weapons when the user left clicks
        if (Input.GetMouseButtonUp(0) && timer > AttackCooldown)
        {
            timer = 0.0f;
            foreach (var position in AttackPositions)
                Instantiate(Attack, position);
        }

        // Speed up when W is held down
        // Slow down when S is held down
        CurrentSpeed += Input.GetAxis("Vertical") * BaseSpeed * Time.deltaTime;

        // Prevent moving slower than BaseSpeed
        CurrentSpeed = CurrentSpeed < BaseSpeed ? BaseSpeed : CurrentSpeed;

        if (Boosted)
        {
            // Speed up to the HardSpeedCap
            CurrentSpeed += BaseSpeed * BoostSpeedModifier * Time.deltaTime;
            if (CurrentSpeed > HardSpeedCap)
                CurrentSpeed = HardSpeedCap;
        }
        else // Slow down to the SoftSpeedCap
            if (CurrentSpeed > SoftSpeedCap)
                CurrentSpeed = SoftSpeedCap;

        Vector3 angles = transform.eulerAngles + (Vector3.right * -Input.GetAxis("Mouse Y") + Vector3.up * Input.GetAxis("Mouse X")) * Time.deltaTime * RotateSpeed;
        transform.eulerAngles = angles;
	}

    void FixedUpdate()
    {
        transform.position += transform.forward * CurrentSpeed * Time.fixedDeltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            EnemyAI enemy = other.transform.parent.GetComponent<EnemyAI>();
            if (enemy)
                HP.CurrentHealth -= enemy.AttackDamage;
        }
    }
}

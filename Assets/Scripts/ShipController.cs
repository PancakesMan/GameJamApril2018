using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float BaseSpeed, SoftSpeedCap, HardSpeedCap, CurrentSpeed, BoostSpeedModifier;
    public float RotateSpeed = 1.0f;
    public ParticleSystem Attack;
    public List<Transform> AttackPositions;
    public float AttackCooldown = 1.0f;
    public bool Controllable = true;

    public ParticleSystem DeathExplosion;
    public AudioClip boostedSound, regularSound;

    private float timer = 0.0f;
    private bool Boosted = false;
    private HealthSystem HP;
    public AudioSource[] audioSources;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        HP = GetComponent<HealthSystem>();
        audioSources = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Controllable)
        {
            timer += Time.deltaTime;

            // Toggle speed boost when either shift is pressed
            if (CurrentSpeed >= SoftSpeedCap &&
                Input.GetKeyDown(KeyCode.LeftShift) ||
                Input.GetKeyDown(KeyCode.RightShift))
            {
                Boosted = !Boosted;
                if (Boosted)
                    audioSources[1].clip = boostedSound;
                else
                    audioSources[1].clip = regularSound;

                audioSources[1].Play();

            }

            // Shoot weapons when the user left clicks
            if (Input.GetMouseButtonUp(0) && timer > AttackCooldown)
            {
                timer = 0.0f;
                foreach (var position in AttackPositions)
                {
                    Instantiate(Attack, position);
                    audioSources[0].PlayOneShot(audioSources[0].clip, 5);
                }
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

            float deltaY = Input.GetAxis("Mouse Y"), deltaX = Input.GetAxis("Mouse X");
            Vector3 angles = transform.eulerAngles + (Vector3.right * -deltaY + Vector3.up * deltaX) * Time.deltaTime * RotateSpeed;
            if (angles.x > 180)
                angles.x -= 360;
            angles.x = Mathf.Clamp(angles.x, -70, 70);
            transform.eulerAngles = angles;

            //var r = transform.rotation;
            //r = Quaternion.AngleAxis(deltaX, transform.up) * r;
            //r = Quaternion.AngleAxis(-deltaY, transform.right) * r;
            //transform.rotation = r;
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * CurrentSpeed * Time.fixedDeltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            audioSources[2].PlayOneShot(audioSources[2].clip, 1);
            EnemyAI enemy = other.transform.parent.GetComponent<EnemyAI>();
            if (enemy)
                HP.CurrentHealth -= enemy.AttackDamage;
            else
            {
                CruiserAI cruiser = other.transform.parent.GetComponent<CruiserAI>();
                if (cruiser)
                    HP.CurrentHealth -= cruiser.AttackDamage;
            }
        }
    }
}

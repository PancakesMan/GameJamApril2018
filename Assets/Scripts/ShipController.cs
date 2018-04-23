using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float BaseSpeed, SoftSpeedCap, HardSpeedCap, CurrentSpeed, BoostSpeedModifier;

    private bool Boosted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Activate speed boost when either shift is held down
        if (CurrentSpeed >= SoftSpeedCap &&
            Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.RightShift))
                Boosted = !Boosted;

        // Speed up when W is held down
        // Slow down when S is held down
        CurrentSpeed += Input.GetAxis("Vertical") * BaseSpeed * Time.deltaTime;
        CurrentSpeed = CurrentSpeed < BaseSpeed ? BaseSpeed : CurrentSpeed;

        if (Boosted)
        {
            CurrentSpeed += BaseSpeed * BoostSpeedModifier * Time.deltaTime;
            if (CurrentSpeed > HardSpeedCap)
                CurrentSpeed = HardSpeedCap;
        }
        else
            if (CurrentSpeed > SoftSpeedCap)
                CurrentSpeed = SoftSpeedCap;

	}

    void FixedUpdate()
    {
        transform.position += transform.forward * CurrentSpeed * Time.fixedDeltaTime;
    }
}

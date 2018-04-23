using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float BaseSpeed, SoftSpeedCap, HardSpeedCap, CurrentSpeed, BoostSpeedModifier;
    public float RotateSpeed = 1.0f;

    private bool Boosted = false;
    private float prevMouseX, prevMouseY;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

        // Toggle speed boost when either shift is pressed
        if (CurrentSpeed >= SoftSpeedCap &&
            Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.RightShift))
                Boosted = !Boosted;

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

        float deltaMouseX = Input.mousePosition.x - prevMouseX;
        float deltaMouseY = Input.mousePosition.y - prevMouseY;

        Vector3 angles = transform.eulerAngles + (Vector3.right * -deltaMouseY + Vector3.up * deltaMouseX) * Time.deltaTime * RotateSpeed;
        transform.eulerAngles = angles;

        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;

	}

    void FixedUpdate()
    {
        transform.position += transform.forward * CurrentSpeed * Time.fixedDeltaTime;
    }
}

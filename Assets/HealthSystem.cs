using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {

    public int MaxHealth, CurrentHealth, DeathSceneIndex;
    public float DeathSceneTimeout = 1.0f;
    private int OldHealth = -1;

    public Text DisplayText;
    private ShipController controller;
	void Start () {
        controller = GetComponent<ShipController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentHealth != OldHealth)
        {
            DisplayText.text = "Health: " + (CurrentHealth < 0 ? 0 : CurrentHealth);
            OldHealth = CurrentHealth;
        }

        if (CurrentHealth <= 0)
        {
            if (controller)
            {
                Instantiate(controller.DeathExplosion, transform.position, transform.rotation);
                controller.audioSources[2].PlayOneShot(controller.audioSources[2].clip, 10);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                controller.Controllable = false;
                PlayerPrefs.SetInt("PlayScore", GetComponent<ScoreSystem>().Score);
                Invoke("LoadDeathScene", DeathSceneTimeout);
            }
        }
	}

    private void LoadDeathScene()
    {
        SceneManager.LoadScene(DeathSceneIndex);
    }
}

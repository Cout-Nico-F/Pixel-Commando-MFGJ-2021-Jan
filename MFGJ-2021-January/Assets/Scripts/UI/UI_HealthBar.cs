using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{ 
    public Slider healthSlider;
    PlayerController playerController;

    private void Awake() {
        //Initialize health at full health
        playerController = FindObjectOfType<PlayerController>();
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = playerController.maxHealthPoints;
    }

    public void SetUIHealth(float health, float maxHealth)
    {
        //Health is set to (100,100) upon death. This avoids seeing full health in UI when dying.
        if (playerController.gameObject.activeSelf){
            healthSlider.value = health;
            healthSlider.maxValue = maxHealth;
        }else{
            healthSlider.value = 0;
            healthSlider.maxValue = maxHealth;
        }
    }

    public void SetLives(string lives)
    {
        GetComponentInChildren<Text>().text = lives;
    }

}
